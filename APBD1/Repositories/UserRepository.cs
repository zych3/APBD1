using APBD1.Users;

namespace APBD1.Repositories;

public class UserRepository
{
    private List<User> _dbSet { get; init; } = [];
    public IReadOnlyList<User> Users => _dbSet.AsReadOnly();

    public Guid AddUser(CreateUserDTO dto)
    {
        return dto switch
        {
            CreateStudentDTO studentDto => AddStudent(studentDto),
            CreateEmployeeDTO employeeDto => AddEmployee(employeeDto),
            _ => throw new ArgumentException("Invalid CreateUserDTO")
        };
    }

    private Guid AddEmployee(CreateEmployeeDTO dto)
    {
        if (string.IsNullOrEmpty(dto.Name) ||
            string.IsNullOrEmpty(dto.Surname) ||
            dto.Salary < Employee.MinSalary)
        {
            throw new ArgumentException("Invalid parameters for employee constructor");
        }

        var emp = new Employee
        {
            Name = dto.Name,
            Surname = dto.Surname,
            Salary = dto.Salary
        };
        _dbSet.Add(emp);
        return emp.Id;
    }

    private Guid AddStudent(CreateStudentDTO dto)
    {
        if (string.IsNullOrEmpty(dto.Name) ||
            string.IsNullOrEmpty(dto.Surname) ||
            string.IsNullOrEmpty(dto.IndexNumber))
        {
            throw new ArgumentException("Invalid parameters for student constructor");
        }
        var student = new Student
        {
            IndexNumber = dto.IndexNumber,
            Name = dto.Name,
            Surname = dto.Surname
        };
        _dbSet.Add(student);
        return student.Id;
    }
}

#region DTOs
public abstract class CreateUserDTO
{
    public string Name { get; set; } = string.Empty;
    public string Surname { get; set; } = string.Empty;
}

public class CreateStudentDTO : CreateUserDTO
{
    public string IndexNumber { get; set; } = string.Empty;
}

public class CreateEmployeeDTO : CreateUserDTO
{
    public ulong Salary { get; set; }
}
#endregion
