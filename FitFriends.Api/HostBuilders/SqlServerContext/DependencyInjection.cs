using FitFriends.ServiceLibrary.DataAccess.Contracts;
using FitFriends.ServiceLibrary.DataAccess;

namespace FitFriends.Api.HostBuilders.SqlServerContext
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddSqlServerContext(this IServiceCollection services)
        {
            services.AddSingleton<ISqlDataAccess, SqlDataAccess>();

            return services;
        }
    }
}
