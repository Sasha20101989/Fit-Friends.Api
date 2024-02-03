using FitFriends.ServiceLibrary.Clients;
using FitFriends.ServiceLibrary.Clients.Contracts;
using FitFriends.ServiceLibrary.Domains;
using FitFriends.ServiceLibrary.Domains.Contracts;

namespace FitFriends.Api.HostBuilders.Domains
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddDomains(this IServiceCollection services)
        {
            services.AddScoped<IUserService, UserService>();

            services.AddScoped<ICertificateService, CertificateService>();

            services.AddScoped<IMoyskladService, MoyskladService>();

            services.AddScoped<IImageService, ImageService>();

            return services;
        }
    }
}
