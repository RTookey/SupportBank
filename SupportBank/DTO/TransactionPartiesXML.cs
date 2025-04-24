using System.Xml.Serialization;

namespace SupportBank.Utility;

public class TransactionPartiesXML
{
    [XmlElement("From", typeof(string))]
    public String From { get; set; }
        
    [XmlElement("To", typeof(string))]
    public String To { get; set; }
}