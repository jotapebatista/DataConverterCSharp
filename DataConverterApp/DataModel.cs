using Newtonsoft.Json;
using System.Xml.Serialization;

namespace DataConverterApp
{
    [XmlRoot("vehicles")]
    public class DataModel
    {
        [XmlElement("vehicle")]
        public List<Vehicle> Vehicles { get; set; }
    }

    public class Vehicle
    {
        [XmlAttribute("type")]
        public string Type { get; set; }

        [XmlAttribute("id")]
        public string ID { get; set; }

        [XmlElement("motor")]
        public Motor Motor { get; set; }

        [XmlElement("wheels")]
        public Wheels Wheels { get; set; }

        [XmlElement("color")]
        public string Color { get; set; }
    }

    public class Motor
    {
        [XmlAttribute("type")]
        public string Type { get; set; }

        [XmlElement("power")]
        public string Power { get; set; }

        [XmlElement("displacement")]
        public string Displacement { get; set; }
    }

    public class Wheels
    {
        [XmlElement("wheel")]
        [JsonProperty("Wheel")]
        public List<Wheel> WheelList { get; set; }
    }

    public class Wheel
    {
        [XmlAttribute("size")]
        public string Size { get; set; }

        [XmlAttribute("pressure")]
        public string Pressure { get; set; }
    }
}