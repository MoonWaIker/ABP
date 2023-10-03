using ABPWebApi.Models;
using Microsoft.EntityFrameworkCore;

namespace ABPWebApi.Services
{
    public class DataBase : DbContext, IDataBase
    {
        public DbSet<PriceDevice> Price { get; set; }

        private readonly Dictionary<string, decimal> priceProportions = new()
        {
            { "#FF0000", 33.3m },
            { "#00FF00", 33.3m },
            { "#0000FF", 33.3m }
        };

        public DbSet<Device> ButtonColor { get; set; }

        private const string connectionString = "Server=(localdb)\\mssqllocaldb;Database=ABP;Trusted_Connection=True;";

        public DataBase()
        {
            Price = Set<PriceDevice>();
            ButtonColor = Set<Device>();
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            _ = optionsBuilder.UseSqlServer(connectionString);
        }

        public string GetButtonColor(string deviceToken)
        {
            DependencyInjection(deviceToken);

            if (ButtonColor
            .Any(device => device.DeviceToken == deviceToken))
            {
                return ButtonColor
            .First(device => device.DeviceToken == deviceToken)
            .Value;
            }

            string value = Choosing(Price, priceProportions);
            _ = ButtonColor.Add(new()
            {
                DeviceToken = deviceToken,
                Value = value
            });
            _ = SaveChanges();

            return value;
        }

        public string GetPrice(string deviceToken)
        {
            DependencyInjection(deviceToken);

            return string.Empty;
        }

        public Experiment[] GetStatistic()
        {
            return Array.Empty<Experiment>();
        }

        private static void DependencyInjection(string deviceToken)
        {
            if (string.IsNullOrEmpty(deviceToken))
            {
                throw new ArgumentNullException(deviceToken);
            }
        }

        private static string Choosing(DbSet<PriceDevice> Table, Dictionary<string, decimal> proportions)
        {
            if (!Table.Any())
            {
                return proportions.First().Key;
            }

            foreach (KeyValuePair<string, decimal> item in proportions)
            {
                if (Table.Count(device => device.Value == item.Key) / Table.Count() * 100 < item.Value)
                {
                    return item.Key;
                }
            }

            return proportions.First().Key;
        }
    }
}