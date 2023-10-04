namespace ABPWebApi.Models
{
    public class PriceExperiment : Experiment
    {
        public PriceDevice[] Devices { get; set; } = Array.Empty<PriceDevice>();
    }
}