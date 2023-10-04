using System.ComponentModel.DataAnnotations.Schema;

namespace ABPWebApi.Models
{
    [Table("ButtonColor")]
    public class ButtonColorDevice : Device
    {
        public required string Value { get; set; }
    }
}