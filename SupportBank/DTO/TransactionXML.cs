using System.Xml.Serialization;

namespace SupportBank.Utility;

public class TransactionXML
{
        [XmlAttribute("Date")]
        public double Date { get; set; }

        [XmlElement("Parties")]
        public TransactionPartiesXML Parties { get; set; }
    
        [XmlElement("Description")]
        public String Narrative { get; set; }

        [XmlElement("Value")]
        public decimal Amount { get; set; }
        
}