using Confiti.MoySklad.Remap.Entities;
using Confiti.MoySklad.Remap.Models;
using FitFriends.ServiceLibrary.QueryFilters.MoySklad;
using FitFriends.ServiceLibrary.QueryParameters;

namespace FitFriends.ServiceLibrary.Clients.Contracts
{
    /// <summary>
    /// Интерфейс для взаимодействия с MoySklad API.
    /// </summary>
    public interface IMoyskladService
    {
        /// <summary>
        /// Получает асинхронно отфильтрованный список ассортимента из MoySklad API.
        /// </summary>
        /// <param name="pagination">Параметры пагинации.</param>
        /// <param name="filters">Параметры фильтрации ассортимента.</param>
        /// <returns>Ответ с результатом запроса и списком ассортимента.</returns>
        Task<ApiResponse<EntitiesResponse<Assortment>>> GetFilteredAssortmentsAsync(PaginationParameters? pagination, AssortmentFilterParameters? filters);
    }
}
