using System.Text.RegularExpressions;

namespace SupportBank.Utility;

public class BankManager
{
    
    public List<Person> Customers { get; set; } = new List<Person>();
    
    public List<Transaction> Transactions { get; set; } = new List<Transaction>();
    
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
        foreach (var item in Transactions)
        {
            Person sender = GetCustomer(item.To);
            sender.AddTransaction(item, true);
            Person receiver = GetCustomer(item.From);
            receiver.AddTransaction(item, false);
        }
    }
    
    public string LoadFile(string fileName)
    {
        List<Transaction> newTransactions = new List<Transaction>();
        newTransactions = FileHandler.LoadFile(fileName);
        if (newTransactions.Count > 0)
        {
            Transactions.AddRange(FileHandler.LoadFile(fileName));
            GetAllCustomers();
            return "File successfully loaded";
        }

        return "File unable to be loaded";

    }

}