using SupportBank.States;
using SupportBank.Utility;

namespace SupportBank;

public class SupportBankApplication
{
    private bool _isRunning = false;
    public State CurrentState { get; set; }
    
    public BankManager BankManager { get; }

    public SupportBankApplication()
    {
        BankManager = new BankManager();
        CurrentState = new MenuState(this);
    }

    public void Run()
    {
        _isRunning = true;
        Console.WriteLine("--- Welcome to the SupportBank ---");
        while (_isRunning)
        {
            CurrentState.Run();
            Console.WriteLine();   
        }
    }

    public void Stop()
    {
        _isRunning = false;

        Console.WriteLine("Goodbye!");
    }
}
    
