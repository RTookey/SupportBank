using SupportBank.Enums;

namespace SupportBank.Utility;

public class BankManager
{
    
    public List<Customer> Customers { get; set; } = new ();
    
    public List<Transaction> Transactions { get; set; } = new ();
    
    public Customer GetCustomer(String name)
    {
        if (Customers.Any(c => c.Name == name))
        {
            return Customers.First(c => c.Name == name);
        }
        Customer newCustomer = new Customer(name);
        Customers.Add(newCustomer);
        return newCustomer;
    }
    
    public void LoadAllCustomers()
    {
        foreach (var item in Transactions)
        {
            Customer sender = GetCustomer(item.To);
            sender.AddTransaction(item, true);
            Customer receiver = GetCustomer(item.From);
            receiver.AddTransaction(item, false);
        }
    }
    
    public string LoadFile(string fileName)
    {
        List<Transaction> newTransactions = new List<Transaction>();
        newTransactions = LoadFileByType(fileName);
        if (newTransactions.Count > 0)
        {
            Transactions.AddRange(newTransactions);
            LoadAllCustomers();
            return "File successfully loaded";
        }
        return "No data loaded";

    }

    public List<Transaction> LoadFileByType(string fileName)
    {
        int position = fileName.LastIndexOf('.');
        string fileEnding = fileName.Substring(position + 1).ToUpper();
        if (Enum.TryParse(fileEnding, out FileType fileEndingEnum))
        {
            switch (fileEndingEnum)
            {
                case FileType.CSV:
                    return FileHandler.ReadAllTransactionsCsv(fileName);
                case FileType.JSON:
                    return FileHandler.ReadAllTransactionsJson(fileName);
                case FileType.XML:
                    return FileHandler.ReadAllTransactionsXml(fileName);
                default:
                    throw new ArgumentOutOfRangeException(nameof(FileType), $"Not valid filetype: {fileEndingEnum}");
            }
        }
        return new List<Transaction>();
    }
    
    
    public void WriteFile(FileType fileType)
    {
        switch (fileType)
        {
            case FileType.CSV:
                FileHandler.WriteAllTransactionsCsv(Transactions);
                break; 
            case FileType.JSON:
                FileHandler.WriteAllTransactionsJson(Transactions);
                break;
            case FileType.XML:
                FileHandler.WriteAllTransactionsXml(Transactions);
                break;
            default:
                throw new ArgumentOutOfRangeException(nameof(FileType), $"Not valid filetype: {fileType}");
        }
    }
}   