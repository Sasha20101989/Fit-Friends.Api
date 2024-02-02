using FitFriends.Api.Middleware;

namespace FitFriends.Api.HostBuilders.Handlers
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddExceptionHandler(this IServiceCollection services)
        {
            services.AddTransient<GlobalExceptionHandlingMiddleware>();

            return services;
        }
    }
}
