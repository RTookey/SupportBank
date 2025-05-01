using System.Text.Json;
using System.Text.RegularExpressions;
using System.Xml.Linq;
using System.Xml.Serialization;
using NLog;

namespace SupportBank.Utility;

public static class FileHandler
{
    
    private static readonly ILogger logger = LogManager.GetCurrentClassLogger();
    
    public static bool ValidAmount(string value)
    {
        return Decimal.TryParse(value, out _); 
    }

    public static Transaction CsvToTransaction(string[] values)
    {
        Transaction newTransaction = new Transaction()
        {
            Date = DateConvertor.ParseDate(values[0]),
            From = values[1],
            To = values[2],
            Narrative = values[3],
            Amount = Decimal.Parse(values[4])
        };
        return newTransaction;
    }
    
    public static List<Transaction> ReadAllTransactionsCsv(string filePath)
    {
        List<Transaction> transactions = new List<Transaction>();

        int lineNumber = 0;
        try
        {
            using (StreamReader reader = new StreamReader(filePath))
            {
                while (!reader.EndOfStream)
                {
                    lineNumber++;
                    string line = reader.ReadLine();
                    string[] values = line.Split(',');
                    if (ValidAmount(values[4]))
                    {
                        transactions.Add(CsvToTransaction(values));
                    }
                    else
                    {
                        logger.Log(LogLevel.Error, "Could not parse transaction number: " + line);
                    }
                }
            }
        }
        catch (Exception e)
        {
            logger.Log(LogLevel.Error, "Error reading CSV file" + e.Message);
        }

        return transactions;
    }
    
    public static List<Transaction> ReadAllTransactionsXml(string fileName)
    {
        List<Transaction> transactions = new List<Transaction>();
        var serializer = new XmlSerializer(typeof(TransactionList));

        try
        {
            using (var reader = new StreamReader(fileName))
            {
                TransactionList transactionsXML = (TransactionList)serializer.Deserialize(reader);
                transactions = new List<Transaction>();
                foreach (var item in transactionsXML.Transactions)
                {
                    Transaction currentTransaction = new Transaction(item);
                    transactions.Add(currentTransaction);
                }
            }
        }
        catch (Exception e)
        {
            logger.Log(LogLevel.Error, "Could not parse Json object. Error " + e.Message);
        }
        return transactions;
        
    }
    
    
    public static List<Transaction> ReadAllTransactionsJson(string filePath)
    {
        List<Transaction> transactions = new List<Transaction>();
        try
        {
            string jsonString = File.ReadAllText(filePath);
            transactions = JsonSerializer.Deserialize<List<Transaction>>(jsonString);
        }
        catch (Exception e)
        {
            logger.Log(LogLevel.Error, "Could not parse Json object. Error " + e.Message);
        } 
        return transactions;
    }

    

    public static void WriteAllTransactionsCsv(List<Transaction> transactions)
    {
        try
        {
            using (StreamWriter writer = new StreamWriter("./Resources/AllTransactions.csv"))
            {
                writer.WriteLine("Date,FromAccount,ToAccount,Narrative,Amount");
                foreach (var item in transactions)
                {
                    writer.WriteLine($"{item.Date},{item.From},{item.To},{item.Narrative},{item.Amount}");
                }
            }
        }
        catch (Exception e)
        {
            logger.Log(LogLevel.Error, "Error writing to CSV file: " + e.Message);
        }
    }
    
    public static void WriteAllTransactionsJson(List<Transaction> transactions)
    {
        try
        {
            string json = JsonSerializer.Serialize(transactions);
            File.WriteAllText("./Resources/AllTransactions.json", json);
        }
        catch (Exception e)
        {
            logger.Log(LogLevel.Error, "Error writing to JSON file: " + e.Message);
        }
    }

    public static void WriteAllTransactionsXml(List<Transaction> transactions)
    {
        try
        {
            var xmlTransactions = new XDocument(
                new XElement("AllTransactions",
                    from transact in transactions
                    select new XElement("Transaction",
                        new XElement("Date", transact.Date),
                        new XElement("From", transact.From),
                        new XElement("To", transact.To),
                        new XElement("Narrative", transact.Narrative),
                        new XElement("Amount", transact.Amount)
                    )));
            xmlTransactions.Save("./Resources/AllTransactions.xml");
        }
        catch (Exception e)
        {
            logger.Log(LogLevel.Error, "Error writing to XML file: " + e.Message);
        }
        
    }

}