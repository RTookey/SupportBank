using NLog;

namespace SupportBank.Utility;

public static class FileHandler
{
    
    private static readonly ILogger logger = LogManager.GetCurrentClassLogger();
    
    public static DateTime ParseDate(string date)
    {
        if (DateTime.TryParse(date, out DateTime convertedDate))
        {
            return convertedDate;
        }
        return DateTime.MinValue; 
    }

    public static void ReadAllTransactionsJson(string filePath)
    {
        
    }
    
    
    public static List<Transaction> ReadAllTransactionsCSV(String path)
    {
        List<Transaction> transactions = new List<Transaction>();

        int lineNumber = 0;
        using (StreamReader reader = new StreamReader(path))
        {
            while (!reader.EndOfStream)
            {
                lineNumber++;
                string line = reader.ReadLine();
                try
                {
                    string[] values = line.Split(',');
                    Transaction newTransaction = new Transaction();
                    newTransaction.Date = ParseDate(values[0]);
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