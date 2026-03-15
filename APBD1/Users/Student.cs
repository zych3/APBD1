namespace APBD1.Users;

public class Student : User
{
    public override UserType UserType => UserType.Student;
    public string IndexNumber { get; set; } = string.Empty;
    public ushort YearOfStudy { get; set; } = 1;
}