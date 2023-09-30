using System.Xml.Linq;

namespace ABPWebApi.Models
{
    public class Experiment
    {
        public required string Name { get; set; }

        public int Count { get; set; }

        public Dictionary<string, KeyValuePair<XName, string>> Devices { get; set; } = new();
    }
}