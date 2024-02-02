using Microsoft.OpenApi.Models;

namespace FitFriends.Api.HostBuilders.Documentation
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddDocumentation(this IServiceCollection services)
        {
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo
                {
                    Title = "REST API FIT FRIENDS",
                    Version = "v1",
                    Description = "REST, CORS, Routing",
                    Contact = new OpenApiContact
                    {
                        Name = "Александр Фелюгин"
                    }
                });
            });

            return services;
        }
    }
}
