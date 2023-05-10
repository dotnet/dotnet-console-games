using System.Xml;
using System.Xml.Serialization;

namespace Oligopoly.Source
{
    [XmlRoot("Events")]
    public class Event
    {
        // Create a class fields.
        private int effect;
        private string target;
        private string type;
        private string title;
        private string content;

        [XmlElement("Effect")]
        public int Effect
        {
            get
            {
                return effect;
            }
            set
            {
                if (value <= 0)
                {
                    throw new InvalidOperationException("Effect cannot be less or equal to zero.");
                }
                else
                {
                    effect = value;
                }
            }
        }

        [XmlElement("Target")]
        public string Target
        {
            get
            {
                return target;
            }
            set
            {
                if (string.IsNullOrEmpty(value))
                {
                    throw new InvalidOperationException("Target cannot be null or whitespace.");
                }
                else
                {
                    target = value;
                }
            }
        }

        [XmlElement("Type")]
        public string Type
        {
            get
            {
                return type;
            }
            set
            {
                if (string.IsNullOrEmpty(value))
                {
                    throw new InvalidOperationException("Type cannot be null or whitespace.");
                }
                else if (value != "Positive" && value != "Negative")
                {
                    throw new InvalidOperationException("Type cannot contain value other then Positive or Negative");
                }
                else
                {
                    type = value;
                }
            }
        }

        [XmlElement("Title")]
        public string Title
        {
            get
            {
                return title;
            }
            set
            {
                if (string.IsNullOrEmpty(value))
                {
                    throw new InvalidOperationException("Title cannot be null or whitespace.");
                }
                else
                {
                    title = value;
                }
            }
        }

        [XmlElement("Content")]
        public string Content
        {
            get
            {
                return content;
            }
            set
            {
                if (string.IsNullOrEmpty(value))
                {
                    throw new InvalidOperationException("Content cannot be null or whitespace.");
                }
                else
                {
                    content = value;
                }
            }
        }
    }
}