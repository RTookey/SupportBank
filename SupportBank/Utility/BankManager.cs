using System.Text.RegularExpressions;

namespace SupportBank.Utility;

public class BankManager
{
    
    public List<Person> Customers { get; set; } = new List<Person>();
    
    public List<Transaction> Transactions { get; set; } = new List<Transaction>();

    public BankManager()
    {   
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
    

    public void LoadFile(string fileName)
    {
        Regex csvRegex = new Regex(@"\.csv$", RegexOptions.IgnoreCase);
        if (csvRegex.IsMatch(fileName))
        {
            Transactions.AddRange(FileHandler.ReadAllTransactionsCSV(fileName));
        }
        Regex jsonRegex = new Regex(@"\.json", RegexOptions.IgnoreCase);
        if (jsonRegex.IsMatch(fileName))
        {
            Transactions.AddRange(FileHandler.ReadAllTransactionsJson(fileName));
        }
        Regex xmlRegex = new Regex(@"\.xml", RegexOptions.IgnoreCase);
        if (xmlRegex.IsMatch(fileName))
        {
            Transactions.AddRange(FileHandler.ReadAllTransactionsXML(fileName));
        }
        GetAllCustomers();
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
    
}