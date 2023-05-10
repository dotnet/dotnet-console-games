using System.Xml;
using System.Xml.Serialization;

namespace Oligopoly.Source
{
    [XmlRoot("Company")]
    public class Company
    {
        // Create a class fields.
        private string name;
        private string ticker;
        private string industry;
        private string description;
        private double sharePrice;
        private double shareAmount;

        [XmlElement("Name")]
        public string Name
        {
            get
            {
                return name;
            }
            set
            {
                if (string.IsNullOrWhiteSpace(value))
                {
                    throw new InvalidOperationException("Name cannot be null or whitespace.");
                }
                else
                {
                    name = value;
                }
            }
        }

        [XmlElement("Ticker")]
        public string Ticker
        {
            get
            {
                return ticker;
            }
            set
            {
                if (string.IsNullOrEmpty(value))
                {
                    throw new InvalidOperationException("Ticker cannot be null or whitespace.");
                }
                else
                {
                    ticker = value;
                }
            }
        }

        [XmlElement("Industry")]
        public string Industry
        {
            get
            {
                return industry;
            }
            set
            {
                if (string.IsNullOrEmpty(value))
                {
                    throw new InvalidOperationException("Industry cannot be null of whitespace.");
                }
                else
                {
                    industry = value;
                }
            }
        }

        [XmlElement("SharePrice")]
        public double SharePrice
        {
            get
            {
                return sharePrice;
            }
            set
            {
                if (value <= 0)
                {
                    throw new InvalidOperationException("Share price cannot be less or equal to zero.");
                }
                else
                {
                    sharePrice = value;
                }
            }
        }

        public double ShareAmount
        {
            get
            {
                return shareAmount;
            }
            set
            {
                if (value < 0)
                {
                    throw new InvalidOperationException("Share amount cannot be less than a zero.");
                }
                else
                {
                    shareAmount = value;
                }
            }
        }

        [XmlElement("Description")]
        public string Description
        {
            get
            {
                return description;
            }
            set
            {
                if (string.IsNullOrEmpty(value))
                {
                    throw new InvalidOperationException("Description cannot be null or whitespace.");
                }
                else
                {
                    description = value;
                }
            }
        }
    }
}