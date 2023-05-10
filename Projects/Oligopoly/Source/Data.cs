using System.Xml.Serialization;

namespace Oligopoly.Source
{
    [XmlRoot("Data")]
    public class Data
    {
        [XmlArray("Companies")]
        [XmlArrayItem("Company")]
        public List<Company> gameCompanies { get; set; }

        [XmlArray("Events")]
        [XmlArrayItem("Event")]
        public List<Event> gameEvents { get; set; }
    }
}