using System.Xml.Serialization;

namespace SupportBank.Utility;

[XmlRoot("TransactionList")]
public class TransactionList
{

    [XmlElement("SupportTransaction")]
    public List<TransactionXML> Transactions { get; set; } = new List<TransactionXML>();
}