namespace ABPWebApi.Models
{
    public class Experiments
    {
        public required PriceExperiment PriceExperiment { get; set; }

        public required ButtonColorExperiment ButtonColorExperiment { get; set; }
    }
}