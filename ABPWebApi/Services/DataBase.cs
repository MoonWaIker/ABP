using ABPWebApi.Models;
using Microsoft.EntityFrameworkCore;

namespace ABPWebApi.Services
{
    public class DataBase : DbContext, IDataBase
    {
        public DbSet<ButtonColorDevice> ButtonColor { get; set; }

        private readonly Dictionary<string, decimal> buttonColorProportions = new()
        {
            { "#FF0000", 33.3m },
            { "#00FF00", 33.3m },
            { "#0000FF", 33.3m }
        };

        public DbSet<PriceDevice> Price { get; set; }

        private readonly Dictionary<int, int> priceProportions = new()
        {
            { 10, 75 },
            { 20, 10 },
            { 50, 5 },
            { 5, 10 }
        };

        private const string connectionString = "Server=(localdb)\\mssqllocaldb;Database=ABP;Trusted_Connection=True;";

        public DataBase()
        {
            ButtonColor = Set<ButtonColorDevice>();
            Price = Set<PriceDevice>();
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            _ = optionsBuilder.UseSqlServer(connectionString);
        }

        public int GetPrice(string deviceToken)
        {
            DependencyInjection(deviceToken);

            if (Price
            .Any(device => device.DeviceToken == deviceToken))
            {
                return Price
            .First(device => device.DeviceToken == deviceToken)
            .Value;
            }

            int value = ChoosingPrice(Price, priceProportions);
            _ = Price.Add(new()
            {
                DeviceToken = deviceToken,
                Value = value
            });
            _ = SaveChanges();

            return value;
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

            string value = ChoosingButtonColor(ButtonColor, buttonColorProportions);
            _ = ButtonColor.Add(new()
            {
                DeviceToken = deviceToken,
                Value = value
            });
            _ = SaveChanges();

            return value;
        }

        public Experiments GetStatistic()
        {
            PriceExperiment price = new()
            {
                Name = "price",
                Count = Price.Count(),
                Devices = Price.ToArray()
            };

            ButtonColorExperiment buttonColor = new()
            {
                Name = "button-color",
                Count = ButtonColor.Count(),
                Devices = ButtonColor.ToArray()
            };

            return new()
            {
                PriceExperiment = price,
                ButtonColorExperiment = buttonColor
            };
        }

        private static void DependencyInjection(string deviceToken)
        {
            if (string.IsNullOrEmpty(deviceToken))
            {
                throw new ArgumentNullException(deviceToken);
            }
        }

        private static string ChoosingButtonColor(IEnumerable<ButtonColorDevice> Table, Dictionary<string, decimal> proportions)
        {
            if (!Table.Any())
            {
                return proportions.First().Key;
            }

            foreach (KeyValuePair<string, decimal> item in proportions)
            {
                if ((decimal)Table.Count(device => device.Value == item.Key) / (decimal)Table.Count() * 100 < item.Value)
                {
                    return item.Key;
                }
            }

            return proportions.First().Key;
        }

        private static int ChoosingPrice(IEnumerable<PriceDevice> Table, Dictionary<int, int> proportions)
        {
            if (!Table.Any())
            {
                return proportions.First().Key;
            }

            foreach (KeyValuePair<int, int> item in proportions)
            {
                if ((decimal)Table.Count(device => device.Value == item.Key) / (decimal)Table.Count() * 100 < item.Value)
                {
                    return item.Key;
                }
            }

            return proportions.First().Key;
        }
    }
}