using APBD1.Equipment;
using APBD1.Repositories;

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

        foreach (var device in deviceRepo.DbSet)
        {
            Console.WriteLine($"{device.Id}: {device.Name}");
        }

        foreach (var user in userRepo.DbSet)
        {
            Console.WriteLine($"{user.Id}: {user.Name} {user.Surname}");
        }
    }
}