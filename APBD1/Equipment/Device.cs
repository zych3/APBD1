namespace APBD1.Equipment;

public abstract class Equipment
{
    public Guid Id { get; init; } =  Guid.NewGuid();
    public string Name { get; set; }
    public AvailabilityStatus Status { get; set; }
    
}

public enum AvailabilityStatus
{
    Available,
    Leased
}