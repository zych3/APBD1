using System.CommandLine;
using APBD1.Repositories;
using APBD1.Users;

namespace APBD1.Commands;

public abstract class AddUserCommand<T> : Command where T : User
{
    protected UserRepository _userRepository = null!;
    private Option<string> _userName = new("--name", "-n")
    {
        Required = true, Description = "The name of the user"
    };
    private Option<string> _userSurname = new("--surname", "-s")
    {
        Required = true,
        Description = "The surname of the user"
    };
    protected Action<ParseResult> Handler = null!;
    protected AddUserCommand(string name, string? description = null) : base(name, description)
    {
        Add(_userName);
        Add(_userSurname);
    }
}

public class AddStudentCommand : AddUserCommand<Student>
{
    private Option<string> _indexNr  = new("--index-number", "-i")
    {
        Required = true,
        Description = "The index number of the student"
    };

    public AddStudentCommand(UserRepository repo, string name, string? description = null) : base(name, description)
    {
        Add(_indexNr);
        _userRepository = repo;
        Handler = result =>
        {
            var guid = repo.AddUser(new CreateStudentDTO
            {
                IndexNumber = result.GetRequiredValue<string>("--index-number"),
                Name = result.GetRequiredValue<string>("--name"),
                Surname = result.GetRequiredValue<string>("--surname")
            });
            Console.WriteLine($"Added user with ID: {guid}");
        };
        SetAction(Handler);
    }
}

