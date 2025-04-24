using System.Text.Json.Serialization;
using System.Xml.Serialization;
using SupportBank.Utility;
    
namespace SupportBank.Utility;

public class Transaction
{

    [JsonPropertyName("Date")]
    public DateTime Date { get; set; }
    
    [JsonPropertyName("FromAccount")]
    public String From { get; set; }
    
    [JsonPropertyName("ToAccount")]
    public String To { get; set; }
    
    [JsonPropertyName("Narrative")]
    public String Narrative { get; set; }

    [JsonPropertyName("Amount")]
    public decimal Amount { get; set; }

    public Transaction()
    {
        
    }

    public Transaction(TransactionXML transaction)
    {
        Date = DateTime.FromOADate(transaction.Date);
        From = transaction.Parties.From;
        To = transaction.Parties.To;
        Narrative = transaction.Narrative;
        Amount = transaction.Amount;
    }
    
    public override string ToString()
    {
        return $"{Date} - From:{From} - To:{To} - Narrative:{Narrative} - Amount:{Amount}";
    }
}