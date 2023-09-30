namespace ABPWebApi.Services
{
    public static class ServiceProviderExtensions
    {
        public static void AddServices(this IServiceCollection services)
        {
            _ = services.AddTransient(typeof(IDataBase), typeof(DataBase));
        }
    }
}