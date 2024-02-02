using FitFriends.ServiceLibrary.Entities;

namespace FitFriends.ServiceLibrary.Repositories.Contracts
{
    /// <summary>
    /// Интерфейс репозитория сертификатов.
    /// </summary>
    public interface ICertificateRepository
    {
        /// <summary>
        /// Удалить сертификат по идентификатору.
        /// </summary>
        /// <param name="certificateId">Идентификатор сертификата.</param>
        /// <returns>Задача, представляющая асинхронную операцию удаления.</returns>
        Task DeleteAsync(Guid certificateId);

        /// <summary>
        /// Получить все сертификаты пользователя.
        /// </summary>
        /// <param name="userId">Идентификатор пользователя.</param>
        /// <returns>Задача, представляющая асинхронную операцию получения списка сертификатов.</returns>
        Task<IEnumerable<CertificateEntity>> GetAllByUserAsync(Guid userId);

        /// <summary>
        /// Получить сертификат по идентификатору.
        /// </summary>
        /// <param name="certificateId">Идентификатор сертификата.</param>
        /// <returns>Задача, представляющая асинхронную операцию получения сертификата.</returns>
        Task<CertificateEntity> GetByIdAsync(Guid certificateId);

        /// <summary>
        /// Вставить новый сертификат.
        /// </summary>
        /// <param name="entity">Экземпляр сущности сертификата.</param>
        /// <returns>Задача, представляющая асинхронную операцию вставки сертификата.</returns>
        Task InsertAsync(CertificateEntity entity);

        /// <summary>
        /// Обновить сертификат.
        /// </summary>
        /// <param name="entity">Экземпляр сущности сертификата.</param>
        /// <returns>Задача, представляющая асинхронную операцию обновления и возвращения обновленного сертификата.</returns>
        Task<CertificateEntity> UpdateAsync(CertificateEntity entity);
    }
}
