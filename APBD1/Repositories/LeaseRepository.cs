using APBD1.Equipment;
using APBD1.Leases;
using APBD1.Services;
using APBD1.Users;

namespace APBD1.Repositories;

public class LeaseRepository
{
    private List<Lease> _dbSet { get; init; } = [];
    public IReadOnlyList<Lease> Lease => _dbSet.AsReadOnly();

    public Guid AddLease(CreateLeaseParams opts)
    {
        if (opts.User is null)
        {
            throw new Exception("User not found");
        }
        if (opts.Device is null)
        {
            throw new Exception("Device not found");
        }
        var l = new Lease
        {
            EndDate = opts.dto.EndDate,
            UserId = opts.dto.UserId,
            DeviceId = opts.dto.DeviceId,
            Device = opts.Device,
            Leaser = opts.User,
        };
        _dbSet.Add(l);
        return l.Id;
    }

    public void EndLease(Guid leaseId)
    {
        var active =  _dbSet.FirstOrDefault(l => l.Id == leaseId);
        if (active is null)
        {
            throw new Exception("Lease not found");
        }
        active.EndDate = DateTimeOffset.UtcNow;
    }
}

public class CreateLeaseParams
{
    public CreateLeaseDTO dto { get; set; }

    public User User { get; set; }
    public Device Device { get; set; }
}