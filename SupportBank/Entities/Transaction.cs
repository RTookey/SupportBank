namespace SupportBank.Utility;

public class Transaction
{
    public DateTime Date { get; set; }

    public String From { get; set; }
    
    public String To { get; set; }
    
    public String Narrative { get; set; }

    public decimal Amount { get; set; }

    public override string ToString()
    {
        return $"{Date} - From:{From} - To:{To} - Narrative:{Narrative} - Amount:{Amount}";
    }
}