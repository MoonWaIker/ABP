using ABPWebApi.Models;

namespace ABPWebApi.Services
{
    public class DataBase : IDataBase
    {
        public string GetButtonColor(string deviceToken)
        {
            DependencyInjection(deviceToken);

            return string.Empty;
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
    }
}