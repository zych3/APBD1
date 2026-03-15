namespace APBD1.Equipment;

public class Laptop : Device
{
    public ScreenRatio ScreenRatio { get; set; } = new();
    public CpuVendor CpuVendor { get; set; }
}

public enum CpuVendor
{
    Intel,
    Amd
}

public enum CpuArchitecture
{
    X86,
    X64,
    Arm
}