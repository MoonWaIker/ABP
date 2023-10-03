namespace ABPWebApi.Models
{
    public class Experiment
    {
        public required string Name { get; set; }

        public int Count { get; set; }

        public Device[] Devices { get; set; } = Array.Empty<Device>();
    }
}