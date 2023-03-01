using Hotels.Data.Database;
using Hotels.Data.Repositores;
using Microsoft.Extensions.DependencyInjection;

namespace Hotels.Data
{
    public static class DataServiceCollectionExtensions
    {
        public static IServiceCollection AddDataLayer(this IServiceCollection services, string connectionString)
        {
            services.AddSingleton<IDbConnectionFactory>(_ => new MsSqlConnection(connectionString));

            services.AddSingleton<IHotelRepository, HotelRepository>();

            services.AddSingleton<DbInitializer>();

            return services;
        }
    }
}
