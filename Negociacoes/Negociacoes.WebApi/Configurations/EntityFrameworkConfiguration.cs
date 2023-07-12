using Negociacoes.WebApi.Infra;
using System.Data.Entity;

namespace Negociacoes.WebApi.Configurations
{
    public static class EntityFrameworkConfiguration
    {
        public static void AddEntityFrameworkConfiguration(this IServiceCollection services, IConfiguration configuration)
        {
            var connectionString = configuration.GetConnectionString("SqlServerConnection");

            services.AddScoped(_ => new ApplicationContext(connectionString));
            services.AddScoped<DbContext>(x => x.GetRequiredService<ApplicationContext>());
        }
    }
}
