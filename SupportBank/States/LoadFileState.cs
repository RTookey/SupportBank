namespace SupportBank.States;

public class LoadFileState : State
{
    public LoadFileState(SupportBankApplication application) : base(application)
    {
    }

    public override void Run()
    {
        Console.WriteLine("Enter the path and name of the file you would like to load");
        String userInput = Console.ReadLine();
        String userInputTrim = userInput.Trim();
        if (File.Exists(userInputTrim))
        {
            string successMessage = _application.BankManager.LoadFile(userInputTrim);
            Console.WriteLine(successMessage);
        }
        else
        {
            Console.WriteLine("File not found");
        }
        _application.CurrentState = new MenuState(_application);
    }

}