namespace SupportBank.Utility;

public static class DateConvertor
{
    public static DateTime ParseDate(string date)
    {
        if (DateTime.TryParse(date, out DateTime convertedDate))
        {
            return convertedDate;
        }
        return DateTime.MinValue; 
    }

}