using APBD1.Equipment;
using APBD1.Repositories;
using APBD1.Users;

namespace APBD1.Tests;

public class Tests
{
    [Fact]
    public void CreateUserTests()
    {
        var userRepo = new UserRepository();
        userRepo.AddUser(new CreateStudentDTO
        {
            Name = "Tomasz",
            Surname = "Michałowski",
            IndexNumber = "s2137"
        });
        userRepo.AddUser(new CreateEmployeeDTO
        {
            Name = "Jonasz",
            Surname = "Koran-Mekka",
            Salary = 420_000
        });
        Assert.Equal(2, userRepo.Users.Count);
        Assert.Single(userRepo.Users.OfType<Student>());
        Assert.Single(userRepo.Users.OfType<Employee>());
    }
    
    [Fact]
    public void CreateDeviceTests()
    {
        var deviceRepo = new DeviceRepository();
        deviceRepo.AddDevice(new CreateCameraDTO
        {
            LensModel = "Leica x24",
            MaxZoom = 10,
            Name = "PJA-CAM-01"
        });
        deviceRepo.AddDevice(new CreateLaptopDTO
        {
            CpuVendor = CpuVendor.Amd,
            Name = "PJA-LAP-01",
            ScreenX = 1280,
            ScreenY = 720
        });
        deviceRepo.AddDevice(new CreateProjectorDTO
        {
            MaxBrightness = 1200,
            MaxScreenX = 4000,
            MaxScreenY = 2000,
            Name = "PJA-PROJ-01"
        });
        
        Assert.Equal(3, deviceRepo.Devices.Count);
        Assert.Single(deviceRepo.Devices.OfType<Camera>());
        Assert.Single(deviceRepo.Devices.OfType<Laptop>());
        Assert.Single(deviceRepo.Devices.OfType<Projector>());
    }
}