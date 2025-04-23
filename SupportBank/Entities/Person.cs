namespace SupportBank.Utility;

public class Person
{
    
    public String Name { get; set; }
    
    public List<Transaction> MoneySent { get; private set; } = new List<Transaction>();
    
    public List<Transaction> MoneyReceived { get; private set; } = new List<Transaction>();

    public decimal Money { get; private set; } 

    public Person(String name)
    {
        Name = name;
        Money = 0; 
    }
    
    public void AddTransaction(Transaction transaction, bool IsSent)
    {
        if (IsSent)
        {
            MoneySent.Add(transaction);
            Money -= transaction.Amount;
        }
        else
        {
            MoneyReceived.Add(transaction);
            Money += transaction.Amount;
        }
    }

    public override string ToString()
    {
        return $"Name: {Name}, Money: {Money}";
    }

}