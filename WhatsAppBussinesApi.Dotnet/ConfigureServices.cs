using Microsoft.Extensions.DependencyInjection;
using WhatsAppBussinesApi.Dotnet.Client;

namespace WhatsAppBussinesApi.Dotnet
{
    public static class ConfigureServices
    {
        public static IServiceCollection AddWhatsAppBussinesApi(this IServiceCollection services)
        {
            services.AddScoped<IWhatsAppBusinessClient,  WhatsAppBusinessClient>();

            return services;
        }
    }
}
