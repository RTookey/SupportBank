using System.Text.Json.Serialization;

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

    public override string ToString()
    {
        return $"{Date} - From:{From} - To:{To} - Narrative:{Narrative} - Amount:{Amount}";
    }
}