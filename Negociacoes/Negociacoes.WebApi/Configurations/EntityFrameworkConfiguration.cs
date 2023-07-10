using Microsoft.EntityFrameworkCore;
using Negociacoes.WebApi.Infra;

namespace Negociacoes.WebApi.Configurations
{
    public static class EntityFrameworkConfiguration
    {
        public static void AddEntityFrameworkConfiguration(this IServiceCollection services, IConfiguration configuration)
        {
            var connectionString = configuration.GetConnectionString("SqlServerConnection");

            services.AddDbContext<ApplicationContext>(options => {
                options.UseSqlServer(connectionString).UseSnakeCaseNamingConvention();
            }, ServiceLifetime.Scoped);
        }
    }
}
