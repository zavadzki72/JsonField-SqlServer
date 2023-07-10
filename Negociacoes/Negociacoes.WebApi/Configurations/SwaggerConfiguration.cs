using Microsoft.OpenApi.Models;

namespace Negociacoes.WebApi.Configurations
{
    public static class SwaggerConfiguration
    {
        public static void AddSwaggerConfiguration(this IServiceCollection services)
        {
            services.AddSwaggerGen(s => {

                s.SwaggerDoc("v1", new OpenApiInfo
                {
                    Version = "v1",
                    Title = "Negociacoes API",
                    Description = "API para POC de JSON Field no SqlServer"
                });
            });
        }

        public static void UseSwaggerSetup(this IApplicationBuilder application)
        {
            application.UseSwagger();
            application.UseSwaggerUI(c => {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "Negociacoes API");
            });
        }
    }
}
