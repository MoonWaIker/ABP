using ABPWebApi.Models;

namespace ABPWebApi.Services
{
    public interface IDataBase
    {
        public string GetButtonColor(string deviceToken);

        public string GetPrice(string deviceToken);

        public Experiment[] GetStatistic();
    }
}