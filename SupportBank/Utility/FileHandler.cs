using System.Text.Json;
using System.Xml.Serialization;
using NLog;

namespace SupportBank.Utility;

public static class FileHandler
{
    
    private static readonly ILogger logger = LogManager.GetCurrentClassLogger();
    


    public static List<Transaction> ReadAllTransactionsXML(string fileName)
    {
        var serializer = new XmlSerializer(typeof(TransactionList));
        using (var reader = new StreamReader(fileName))
        {
            TransactionList transactionsXML = (TransactionList)serializer.Deserialize(reader);
            List<Transaction> transactions = new List<Transaction>();

            foreach (var item in transactionsXML.Transactions)
            {
                Transaction currentTransaction = new Transaction(item);
                transactions.Add(currentTransaction);
            }

            return transactions;
        }
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
    
    
    public static List<Transaction> ReadAllTransactionsCSV(String filePath)
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
    
}