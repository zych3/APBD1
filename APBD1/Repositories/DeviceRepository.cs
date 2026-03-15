using APBD1.Equipment;

namespace APBD1.Repositories;

public class DeviceRepository
{
    private List<Device> _dbSet = [];
    public IReadOnlyList<Device> DbSet => _dbSet.AsReadOnly();

    public Guid AddDevice(CreateDeviceDTO dto)
    {
        if (string.IsNullOrEmpty(dto.Name))
        {
            throw new ArgumentException("Device name cannot be null or empty");
        }
        return dto switch
        {
            CreateLaptopDTO laptop => AddLaptop(laptop),
            CreateCameraDTO camera => AddCamera(camera),
            CreateProjectorDTO projector => AddProjector(projector),
            _ => throw new ArgumentException("Invalid DTO for device creation")
        };
    }
    private Guid AddCamera(CreateCameraDTO dto)
    {
        if (string.IsNullOrEmpty(dto.LensModel))
        {
            throw new ArgumentException("LensModel cannot be null or empty");
        }

        var camera = new Camera
        {
            LensModel = dto.LensModel,
            MaxZoom = dto.MaxZoom,
        };
        _dbSet.Add(camera);
        return camera.Id;
    }
    private Guid AddProjector(CreateProjectorDTO dto)
    {
        var proj = new Projector
        {
            MaxBrightness = dto.MaxBrightness,
            MaxScreenRatio = new ScreenRatio
            {
                X = dto.MaxScreenX,
                Y = dto.MaxScreenY,
            }
        };
        _dbSet.Add(proj);
        return proj.Id;
    }
    private Guid AddLaptop(CreateLaptopDTO dto)
    {
        var laptop = new Laptop
        {
            Name = dto.Name,
            ScreenRatio = new ScreenRatio
            {
                X = dto.ScreenX,
                Y = dto.ScreenY,
            },
            CpuVendor = dto.CpuVendor
        };
        _dbSet.Add(laptop);
        return laptop.Id;
    }
}

#region DTOs

public class CreateDeviceDTO
{
    public string Name { get; set; } = string.Empty;
}

public class CreateLaptopDTO : CreateDeviceDTO
{
    public uint ScreenX { get; set; }
    public uint ScreenY { get; set; }
    public CpuVendor CpuVendor { get; set; }
}

public class CreateCameraDTO : CreateDeviceDTO
{
    public string LensModel { get; set; } = string.Empty;
    public uint MaxZoom { get; set; }
}

public class CreateProjectorDTO : CreateDeviceDTO
{
    public uint MaxScreenX { get; set; }
    public uint MaxScreenY { get; set; }
    public ulong MaxBrightness { get; set; }
}
#endregion