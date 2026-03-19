using APBD1.Equipment;
using APBD1.Users;

namespace APBD1.Leases;

public class Lease
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public static readonly double DayLateFee = 0.8;
    public DateTimeOffset StartDate { get; set; } = DateTimeOffset.UtcNow;
    public DateTimeOffset EndDate { get; set; }

    public TimeSpan Duration => ReturnDate is null ?
        TimeSpan.Zero : ReturnDate.Value - StartDate;

    public DateTimeOffset? ReturnDate { get; set; }
    public bool ReturnedOnTime => ReturnDate is null ?
        DateTimeOffset.UtcNow <= EndDate : ReturnDate <= EndDate;
    public double LatePenalty => ReturnedOnTime ? 0 :
        Duration.TotalDays *  DayLateFee;
    #region navigation 
    public Guid UserId { get; set; }
    public User Leaser { get; init; }
    public Guid DeviceId { get; set; }
    public Device Device { get; init; } 
    #endregion
}