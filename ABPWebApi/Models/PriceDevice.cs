using System.ComponentModel.DataAnnotations.Schema;

namespace ABPWebApi.Models
{
    [Table("Price")]
    public class PriceDevice : Device
    {
        public int Value { get; set; }
    }
}