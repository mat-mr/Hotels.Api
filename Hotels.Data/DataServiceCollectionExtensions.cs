using Hotels.Data.Repositores;
using Microsoft.Extensions.DependencyInjection;

namespace Hotels.Data
{
    public static class DataServiceCollectionExtensions
    {
        public static IServiceCollection AddData(this IServiceCollection services)
        {
            services.AddSingleton<IHotelRepository, HotelRepository>();

            return services;
        }
    }
}
