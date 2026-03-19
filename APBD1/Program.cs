using APBD1.Equipment;
using APBD1.Repositories;
using APBD1.Services;

namespace APBD1;

class Program
{
    /*
     * TODO:
     * LeaseRepository
     * Constrains
     * 
     */
    static void Main(string[] args)
    {
        var userRepo = new UserRepository();
        var deviceRepo = new DeviceRepository();

        deviceRepo.AddDevice(new CreateLaptopDTO
        {
            CpuVendor = CpuVendor.Amd,
            Name = "PJA-LAP-01",
            ScreenX = 1280,
            ScreenY = 720,
        });

        userRepo.AddUser(new CreateStudentDTO
        {
            Name = "Tomasz",
            Surname = "Michałowski",
            IndexNumber = "s2137"
        });

        foreach (var device in deviceRepo.Devices)
        {
            Console.WriteLine($"{device.Id}: {device.Name}");
        }

        foreach (var user in userRepo.Users)
        {
            Console.WriteLine($"{user.Id}: {user.Name} {user.Surname}");
        }
        
        var dId = deviceRepo.Devices[0].Id;
        var uId = userRepo.Users[0].Id;

        var leaseRepo = new LeaseRepository();
        var leaseService = new LeaseService(userRepo, deviceRepo, leaseRepo);
        leaseService.CreateLease(new CreateLeaseDTO
        {
            DeviceId = dId,
            UserId = uId,
            EndDate = DateTimeOffset.UtcNow.AddSeconds(30)
        });
        foreach (var l in leaseRepo.Lease)
        {
            Console.WriteLine($"{l.StartDate} - {l.EndDate} : {l.Leaser.Name} leased {l.Device.Name}");
        }
        Console.WriteLine($"Late leases count: {leaseService.GetLateLeases().ToList().Count}");
        Thread.Sleep(TimeSpan.FromSeconds(30));
        Console.WriteLine($"Late leases count: {leaseService.GetLateLeases().ToList().Count}");
    }
}