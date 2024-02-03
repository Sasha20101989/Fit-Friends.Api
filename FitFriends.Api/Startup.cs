using FitFriends.Api.HostBuilders.Documentation;
using FitFriends.Api.HostBuilders.Domains;
using FitFriends.Api.HostBuilders.Handlers;
using FitFriends.Api.HostBuilders.Repositories;
using FitFriends.Api.HostBuilders.SqlServerContext;
using FitFriends.Api.Middleware;
using FitFriends.ServiceLibrary.Configurations;
using Newtonsoft.Json.Converters;
using OfficeOpenXml;

namespace FitFriends.Api
{
    public class Startup(IConfiguration configuration)
    {
        public IConfiguration Configuration { get; } = configuration;

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers().AddNewtonsoftJson(o =>
            {
                o.SerializerSettings.Converters.Add(new StringEnumConverter());
            });

            services.AddSwaggerGenNewtonsoftSupport();

            services.AddEndpointsApiExplorer();

            services.AddDocumentation();

            services.AddRepositories();

            services.AddDomains();

            services.AddLogging();

            services.AddExceptionHandler();

            services.AddSqlServerContext();

            services.Configure<AppConfig>(Configuration);

            ExcelPackage.LicenseContext = LicenseContext.NonCommercial;
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();

            app.UseMiddleware<GlobalExceptionHandlingMiddleware>();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();

                endpoints.MapFallbackToFile("/index.html");
            });
        }
    }
}