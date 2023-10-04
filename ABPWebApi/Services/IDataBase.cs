using ABPWebApi.Models;

namespace ABPWebApi.Services
{
    public interface IDataBase
    {
        public string GetButtonColor(string deviceToken);

        public int GetPrice(string deviceToken);

        public Experiments GetStatistic();
    }
}