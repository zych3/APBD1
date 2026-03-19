using APBD1.Equipment;
using APBD1.Leases;
using APBD1.Repositories;
using APBD1.Users;
using NLog;

namespace APBD1.Services;

public class LeaseService(UserRepository userRepo, DeviceRepository deviceRepo, LeaseRepository leaseRepo)
{
    public static readonly Logger Logger = LogManager.GetCurrentClassLogger();

    public IEnumerable<Lease> GetLeasesForUser(Guid userId)
    {
        return leaseRepo.Lease.Where(l => l.UserId == userId).ToList();
    }

    public IEnumerable<Lease> GetLateLeases()
    {
        return leaseRepo.Lease.Where(l => l.EndDate < DateTimeOffset.UtcNow).ToList();
    }

    public Guid CreateLease(CreateLeaseDTO dto)
    {
        var user = userRepo.Users.FirstOrDefault(u => u.Id == dto.UserId);
        var device  = deviceRepo.Devices.FirstOrDefault(d => d.Id == dto.DeviceId);
        if (!ValidateLeaser(user))
        {
            Logger.Error("User has max leases active.");
        }

        if (!ValidateDevice(device))
        {
            Logger.Error("Device is unavaliable for lease.");
        }
        try
        {
            return leaseRepo.AddLease(new CreateLeaseParams
            {
                dto = dto,
                Device = device,
                User = user
            });
        }
        catch (Exception e)
        {
            Logger.Error(e, $"Lease creation failed: {e.Message}");
        }
        return Guid.Empty;
    }

    private bool ValidateLeaser(User leaser) => leaser switch
    {
        Student => leaseRepo.Lease.Count(l => l.UserId == leaser.Id) <= 2,
        Employee => leaseRepo.Lease.Count(l => l.UserId == leaser.Id) <= 5,
        _ => false
    };

    private bool ValidateDevice(Device device) =>
        deviceRepo.Devices.FirstOrDefault(d => d.Id == device.Id)?.Status == AvailabilityStatus.Available;

    public (bool returnedOnTime, double latePenalty) EndLease(Guid leaseId)
    {
        try
        {
            leaseRepo.EndLease(leaseId);
            var lease =  leaseRepo.Lease.FirstOrDefault(l => l.Id == leaseId);
            if (!lease?.ReturnedOnTime ?? false)
            {
                return (false, lease.LatePenalty);
            }

            return (true, 0);
        }
        catch (Exception e)
        {
            Logger.Error(e, $"Lease end failed: {e.Message}");
            return (false, 0);
        }
    }
}

public class CreateLeaseDTO
{
    public DateTimeOffset EndDate { get; set; }
    public Guid UserId { get; set; }
    public Guid DeviceId { get; set; }
}