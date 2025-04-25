using SupportBank.Enum;

namespace SupportBank.States;

public class WriteFileState : State 
{
    public WriteFileState(SupportBankApplication application) : base(application)
    {
    }

    public override void Run()
    {
        Console.WriteLine("Please select a number for the file type");
        Console.WriteLine("1. CSV");
        Console.WriteLine("2. JSON");
        Console.WriteLine("3. XML");
        string userInput = Console.ReadLine();
        string userInputTrimmed = userInput.Trim();
        if (Int32.TryParse(userInputTrimmed, out int userInputInt) && userInputInt >= 1 && userInputInt <= 3)
        {
            FileType fileType = (FileType)userInputInt;
            _application.BankManager.WriteFile(fileType);
            _application.CurrentState = new MenuState(_application);
        }
        else
        {
            Console.WriteLine("Please enter a valid number for the file type");
        }
    }

}