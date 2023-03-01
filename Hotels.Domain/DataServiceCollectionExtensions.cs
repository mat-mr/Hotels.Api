using FluentValidation;
using Hotels.Services;
using Hotels.Services.Services;
using Microsoft.Extensions.DependencyInjection;

namespace Hotels.Data
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddServiceLayer(this IServiceCollection services)
        {
            services.AddSingleton<IHotelService, HotelService>();
            services.AddValidatorsFromAssemblyContaining<IServicesMarker>(ServiceLifetime.Singleton);

            return services;
        }
    }
}
