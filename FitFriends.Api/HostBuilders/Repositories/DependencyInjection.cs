using FitFriends.ServiceLibrary.Repositories;
using FitFriends.ServiceLibrary.Repositories.Contracts;

namespace FitFriends.Api.HostBuilders.Repositories
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddRepositories(this IServiceCollection services)
        {
            services.AddScoped<IUserRepository, UserRepository>();

            services.AddScoped<ICertificateRepository, CertificateRepository>();

            return services;
        }
    }
}
