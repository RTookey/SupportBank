namespace SupportBank.States;

public class ViewAllState : State
{
    public ViewAllState(SupportBankApplication application) : base(application)
    {
    }

    public override void Run()
    {
        if (_application.BankManager.Customers.Count == 0)
        {
            Console.WriteLine("There are no records to view");
        }
        foreach (var customer in _application.BankManager.Customers)
        {
            Console.WriteLine(customer.ToString());
        }
        Console.WriteLine("\n\n");
        _application.CurrentState = new MenuState(_application);
    }
    
}