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
        Console.WriteLine("3. Exit");
        String userInput = Console.ReadLine();
        if (Int32.TryParse(userInput, out int userInputInt))
        {
            if (userInputInt == 1) _application.CurrentState = new ViewAllState(_application);
            if (userInputInt == 2) _application.CurrentState = new ViewCustomerState(_application);
            if (userInputInt == 3) _application.Stop();
        }
        Console.WriteLine("Sorry, we didn't get that!");
    }
     
}