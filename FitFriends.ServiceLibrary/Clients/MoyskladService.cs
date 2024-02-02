using Confiti.MoySklad.Remap.Api;
using Confiti.MoySklad.Remap.Client;
using Confiti.MoySklad.Remap.Entities;
using Confiti.MoySklad.Remap.Models;
using FitFriends.ServiceLibrary.Clients.Contracts;
using FitFriends.ServiceLibrary.Configurations;
using FitFriends.ServiceLibrary.QueryFilters.MoySklad;
using FitFriends.ServiceLibrary.QueryParameters;
using Microsoft.Extensions.Options;

namespace FitFriends.ServiceLibrary.Clients
{
    /// <summary>
    /// Сервис для взаимодействия с MoySklad API.
    /// </summary>
    public class MoyskladService: IMoyskladService
    {
        private readonly MoySkladApi _api;

        private readonly IOptions<AppConfig> _configuration;

        /// <summary>
        /// Инициализирует новый экземпляр <see cref="MoyskladService"/> class.
        /// </summary>
        /// <param name="configuration">Конфигурация приложения.</param>
        public MoyskladService(IOptions<AppConfig> configuration)
        {
            _configuration = configuration;
            _api = new MoySkladApi(new MoySkladCredentials { AccessToken = _configuration.Value.MoySklad.AccessToken });
        }

        /// <inheritdoc />
        public async Task<ApiResponse<EntitiesResponse<Assortment>>> GetFilteredAssortmentsAsync(PaginationParameters? pagination, AssortmentFilterParameters? filters)
        {
            var query = new AssortmentApiParameterBuilder();

            if (filters is not null)
            {
                if (!string.IsNullOrEmpty(filters.NameFilter))
                {
                    query.Parameter(p => p.Name).Should().Be(filters.NameFilter);
                }

                query.Parameter(p => p.Archived).Should().Be(filters.ArchivedFilter);

                if (filters.UpdatedFilter is not null)
                {
                    query.Parameter(p => p.Updated).Should().BeGreaterOrEqualTo((DateTime)filters.UpdatedFilter);
                }
            }

            query.Order().By(p => p.Name);

            if (pagination is not null)
            {
                query.Limit(pagination.PageSize);
                query.Offset((pagination.PageNumber - 1) * pagination.PageSize);
            }

            return await _api.Assortment.GetAllAsync(query);
        }
    }
}
