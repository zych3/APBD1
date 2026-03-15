namespace APBD1.Equipment;

public abstract class Equipment
{
    public Guid Id { get; init; } =  Guid.NewGuid();
}