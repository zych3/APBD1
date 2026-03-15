namespace APBD1.Equipment;

public class Projector : Device
{
    public ScreenRatio MaxScreenRatio { get; set; } = new();
    public ulong MaxBrightness { get; set; }
}