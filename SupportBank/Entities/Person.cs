namespace SupportBank.Utility;

public class Person
{
    // public long Id { get; set; }
    
    public String Name { get; set; }
    
    private List<Transaction> MoneySent { get; set; }
    
    private List<Transaction> MoneyReceived { get; set; }

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


}