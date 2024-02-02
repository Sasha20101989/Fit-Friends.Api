using FitFriends.ServiceLibrary.Properties;

namespace FitFriends.ServiceLibrary.QueryParameters
{
    /// <summary>
    /// Параметры пагинации.
    /// </summary>
    public class PaginationParameters
    {
        /// <summary>
        /// Размер страницы.
        /// </summary>
        public int PageSize { get; set; } = int.Parse(Resources.DefaultPageSize);

        /// <summary>
        /// Номер страницы.
        /// </summary>
        public int PageNumber { get; set; } = int.Parse(Resources.DefaultPageNumber);
    }
}
