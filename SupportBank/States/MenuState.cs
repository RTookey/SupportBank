namespace SupportBank.States;

public class MenuState : State 
{
    public MenuState(SupportBankApplication application) : base(application)
    {
    }

    public override void Run()
    {
        Console.WriteLine("Please enter a number:");
        Console.WriteLine("1. List all customers");
        Console.WriteLine("2. List all transactions for an account");
        Console.WriteLine("3. Load a file");
        Console.WriteLine("4. Exit");
        String userInput = Console.ReadLine();
        if (Int32.TryParse(userInput, out int userInputInt))
        {
            if (userInputInt == 1) _application.CurrentState = new ViewAllState(_application);
            else if (userInputInt == 2) _application.CurrentState = new ViewCustomerState(_application);
            else if (userInputInt == 3) _application.CurrentState = new LoadFileState(_application);
            else if (userInputInt == 4) _application.Stop();
            else Console.WriteLine("Sorry, we didn't get that!");
        }
        else
        {
            Console.WriteLine("Sorry, we didn't get that!");
        }
    }
     
}