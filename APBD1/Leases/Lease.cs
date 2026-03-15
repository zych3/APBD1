using APBD1.Equipment;
using APBD1.Users;

namespace APBD1.Leases;

public class Lease
{
    public DateTimeOffset StartDate { get; set; } = DateTimeOffset.Now;
    public DateTimeOffset EndDate { get; set; }
    public TimeSpan Duration => EndDate - StartDate;
    public DateTimeOffset? ReturnDate { get; set; }
    public bool ReturnedOnTime { get; init; }
    #region navigation 
    public Guid UserId { get; init; }
    public User Leaser { get; init; }
    public Guid DeviceId { get; init; }
    public Device Device { get; init; } 
    #endregion
}