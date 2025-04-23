using System.Globalization;
using System;
using System.Globalization;
using System.IO;
using CsvHelper; 

namespace SupportBank.Utility;

public static class FileHandler
{
    public const string Path = "Resources/Transactions2014.csv";

    public static List<Transaction> ReadAllTransactions()
    {
        List<Transaction> transactions = new List<Transaction>();
        
        using (var reader = new StreamReader(Path))
        using (var csv = new CsvReader(reader, CultureInfo.InvariantCulture))
        {
            transactions = csv.GetRecords<Transaction>().ToList();
        }
        
        return transactions;
    }
    
}