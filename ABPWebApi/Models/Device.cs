using Microsoft.EntityFrameworkCore;

namespace ABPWebApi.Models
{
    [PrimaryKey(nameof(Id))]
    public class Device
    {
        public int Id { get; set; }

        public required string DeviceToken { get; set; }
    }
}