namespace APBD1.Users;

public class User 
{
    public Guid Id { get; init; } = Guid.NewGuid();
    public string Name { get; init; } = string.Empty;
    public string Surname { get; init; } = string.Empty;
    public virtual UserType UserType { get; set; } 
}

public enum UserType
{
    Student,
    Employee
}