using SupportBank.Utility;

namespace SupportBank.States;

public class ViewCustomerState : State
{
    public ViewCustomerState(SupportBankApplication application) : base(application)
    {
    }

    public override void Run()
    {
        Console.WriteLine("Enter the name of the customer you would like to view transactions for");
        String userInput = Console.ReadLine();
        String userInputTrim = userInput.Trim();
        if (_application.BankManager.Customers.Any(c => c.Name == userInputTrim))
        {
            Customer userCustomer = _application.BankManager.Customers.First(c => c.Name == userInputTrim);
            Console.WriteLine($"Transactions for {userCustomer.ToString()}");
            Console.WriteLine("Money sent:");
            foreach (var transaction in userCustomer.MoneySent)
            {
                Console.WriteLine(transaction.ToString());
            }
            Console.WriteLine("Money received:"); 
            foreach (var transaction in userCustomer.MoneyReceived)
            {
                Console.WriteLine(transaction.ToString());
            }
        }
        else
        {
            Console.WriteLine("That user doesn't exist");
        }
        Console.WriteLine("\n\n");
        _application.CurrentState = new MenuState(_application);
    }
    
}