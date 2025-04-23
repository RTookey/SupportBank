namespace SupportBank.States;

public abstract class State
{
    protected SupportBankApplication _application;

    protected State(SupportBankApplication application)
    {
        _application = application;
    }

    public abstract void Run();

}
