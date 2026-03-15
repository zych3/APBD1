namespace APBD1.Users;

public class Employee :  User
{
    public override UserType UserType => UserType.Employee;
    public ulong Salary { get; set; } = MinSalary;
    public static ulong MinSalary { get; set; }
}