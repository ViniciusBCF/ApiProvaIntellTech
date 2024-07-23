using Microsoft.EntityFrameworkCore;
using ProvaIntellTechApi.Data.Context;
using ProvaIntellTechApi.Data.Repositories;
using ProvaIntellTechApi.Data.Repositories.Interfaces;
using ProvaIntellTechApi.Service.Notifications;
using ProvaIntellTechApi.Service.Notifications.Interfaces;
using ProvaIntellTechApi.Service.Service;
using ProvaIntellTechApi.Service.Service.Interfaces;

namespace ProvaIntellTechApi.Configuration
{
    public static class DependencyInjectionConfig
    {
        public static IServiceCollection ConfigureDb(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<AppDbContext>(options =>
                                                        options.UseSqlServer(configuration
                                                        .GetConnectionString("DefaultConnection")));
            return services;
        }

        public static IServiceCollection AddRepositories(this IServiceCollection repositories)
        {
            repositories.AddScoped<IAtividadeRepository, AtividadeRepository>();
            return repositories;
        }

        public static IServiceCollection AddServices(this IServiceCollection services)
        {
            services.AddScoped<IAtividadeService, AtividadeService>();
            services.AddScoped<INotificador, Notificador>();
            return services;
        }
    }
}
