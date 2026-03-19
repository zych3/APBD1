using APBD1.Equipment;
using APBD1.Repositories;
using APBD1.Services;
using System.CommandLine;
using System.Security.Authentication.ExtendedProtection;
using System.CommandLine;
using System.CommandLine.Parsing;
using APBD1.Commands;

namespace APBD1;

class Program
{
    static void Main(string[] args)
    {
        var userRepo = new UserRepository();
        var rootCmd = new RootCommand("Device leasing manager");
        var addCmd = new Command("add");
        addCmd.Subcommands.Add(new AddStudentCommand(userRepo, "student", "Add a student"));
        rootCmd.Subcommands.Add(addCmd);
        
        Console.WriteLine("Device Leasing Manager. Type 'exit' to quit.");

        while (true)
        {
            Console.Write("> ");
            var line = Console.ReadLine();

            if (line is null || line.Trim().Equals("exit", StringComparison.OrdinalIgnoreCase)
                             || line.Trim().Equals(":q", StringComparison.OrdinalIgnoreCase))
            {
                Console.WriteLine("Bye!");
                break;
            }

            if (string.IsNullOrWhiteSpace(line))
            {
                continue;
            }

            var parsedArgs = SplitArgs(line);
            rootCmd.Parse(parsedArgs).Invoke();
        }
    }
    
    static string[] SplitArgs(string line)
    {
        var args = new List<string>();
        var current = new System.Text.StringBuilder();
        var inQuotes = false;

        foreach (char c in line)
        {
            if (c == '"')
            {
                inQuotes = !inQuotes;
            }
            else if (c == ' ' && !inQuotes)
            {
                if (current.Length > 0)
                {
                    args.Add(current.ToString());
                    current.Clear();
                }
            }
            else
            {
                current.Append(c);
            }
        }

        if (current.Length > 0)
            args.Add(current.ToString());

        return args.ToArray();
    }
}