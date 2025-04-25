using System.Text.Json;
using System.Text.RegularExpressions;
using System.Xml.Serialization;
using NLog;

namespace SupportBank.Utility;

public static class FileHandler
{
    
    private static readonly ILogger logger = LogManager.GetCurrentClassLogger();
    
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
    
    
    public static List<Transaction> ReadAllTransactionsCsv(string filePath)
    {
        List<Transaction> transactions = new List<Transaction>();

        int lineNumber = 0;
        using (StreamReader reader = new StreamReader(filePath))
        {
            while (!reader.EndOfStream)
            {
                lineNumber++;
                string line = reader.ReadLine();
                try
                {
                    string[] values = line.Split(',');
                    Transaction newTransaction = new Transaction();
                    newTransaction.Date = DateConvertor.ParseDate(values[0]);
                    newTransaction.From = values[1];
                    newTransaction.To = values[2];
                    newTransaction.Narrative = values[3];
                    if (Decimal.TryParse(values[4], out Decimal amount))
                    {
                        newTransaction.Amount = amount;
                        transactions.Add(newTransaction);
                    }
                    else
                    {
                        logger.Log(LogLevel.Error, "Could not parse transaction number: " + line);
                    }
                }
                catch (Exception e)
                {
                    logger.Log(LogLevel.Error, $"Could not parse {line}: e.Message");
                }
            }
        }

        return transactions;
    }

    public static void WriteAllTransactionsCsv(List<Transaction> transactions)
    {
        
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
        
    }
    
}