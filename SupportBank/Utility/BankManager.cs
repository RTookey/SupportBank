namespace SupportBank.Utility;

public class BankManager
{
    
    public List<Person> Customers { get; set; } = new List<Person>();
    
    public List<Transaction> Transactions { get; set; }

    public BankManager()
    {   
        GetAllCustomers();
    }

    public Person GetCustomer(String name)
    {
        if (Customers.Any(c => c.Name == name))
        {
            return Customers.First(c => c.Name == name);
        }
        else
        {
            Person newCustomer = new Person(name);
            Customers.Add(newCustomer);
            return newCustomer;
        }
    }

    public void GetAllCustomers()
    {
        Transactions = FileHandler.ReadAllTransactions();
        foreach (var item in Transactions)
        {
            Person sender = GetCustomer(item.To);
            sender.AddTransaction(item, true);
            Person receiver = GetCustomer(item.From);
            receiver.AddTransaction(item, false);
        }
    }
    
}