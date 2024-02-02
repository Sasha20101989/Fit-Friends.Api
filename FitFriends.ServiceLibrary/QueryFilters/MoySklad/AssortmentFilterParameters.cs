namespace FitFriends.ServiceLibrary.QueryFilters.MoySklad
{
    /// <summary>
    /// Параметры фильтрации товаров.
    /// </summary>
    public class AssortmentFilterParameters
    {
        /// <summary>
        /// Фильтр по имени товара.
        /// </summary>
        public string? NameFilter { get; set; }

        /// <summary>
        /// Фильтр по архивированным товарам.
        /// </summary>
        public bool ArchivedFilter { get; set; }

        /// <summary>
        /// Фильтр по дате последнего обновления товара.
        /// </summary>
        public DateTime? UpdatedFilter { get; set; }
    }
}
