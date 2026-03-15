namespace APBD1.Equipment;

public abstract class Device
{
    public Guid Id { get; init; } =  Guid.NewGuid();
    public string Name { get; set; }
    public AvailabilityStatus Status { get; set; } = AvailabilityStatus.Available;
    
}

public enum AvailabilityStatus
{
    Available,
    Leased
}

public class ScreenRatio
{
    public uint X { get; set; }
    public uint Y { get; set; }
}