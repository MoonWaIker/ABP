namespace ABPWebApi.Models
{
    public class ButtonColorExperiment : Experiment
    {
        public ButtonColorDevice[] Devices { get; set; } = Array.Empty<ButtonColorDevice>();
    }
}