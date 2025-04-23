namespace SupportBank.Utility;

public class Transaction
{
    // public long Id { get; set; }
    
    // change this back to date? 
    public String Date { get; set; }

    public String From { get; set; }
    
    public String To { get; set; }
    
    public String Narrative { get; set; }

    public decimal Amount { get; set; }

    public override string ToString()
    {
        return $"{Date} - From:{From} - To:{To} - Narrative:{Narrative} - Amount:{Amount}";
    }
}