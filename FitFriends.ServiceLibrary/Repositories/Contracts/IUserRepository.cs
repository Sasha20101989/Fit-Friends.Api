using FitFriends.ServiceLibrary.Entities;

namespace FitFriends.ServiceLibrary.Repositories.Contracts
{
    /// <summary>
    /// Интерфейс репозитория пользователей.
    /// </summary>
    public interface IUserRepository
    {
        /// <summary>
        /// Удалить пользователя по идентификатору.
        /// </summary>
        /// <param name="userId">Идентификатор пользователя.</param>
        /// <returns>Задача, представляющая асинхронную операцию удаления.</returns>
        Task DeleteAsync(Guid userId);

        /// <summary>
        /// Получить всех пользователей с пагинацией.
        /// </summary>
        /// <param name="pageSize">Размер страницы.</param>
        /// <param name="offset">Смещение.</param>
        /// <returns>Задача, представляющая асинхронную операцию получения списка пользователей.</returns>
        Task<IEnumerable<UserEntity>> GetAllAsync(int pageSize, int offset);

        /// <summary>
        /// Получить пользователя по идентификатору.
        /// </summary>
        /// <param name="userId">Идентификатор пользователя.</param>
        /// <returns>Задача, представляющая асинхронную операцию получения пользователя.</returns>
        Task<UserEntity> GetByIdAsync(Guid userId);

        /// <summary>
        /// Вставить нового пользователя.
        /// </summary>
        /// <param name="entity">Экземпляр сущности пользователя.</param>
        /// <returns>Задача, представляющая асинхронную операцию вставки пользователя.</returns>
        Task InsertAsync(UserEntity entity);

        /// <summary>
        /// Обновить пользователя.
        /// </summary>
        /// <param name="entity">Экземпляр сущности пользователя.</param>
        /// <returns>Задача, представляющая асинхронную операцию обновления и возвращения обновлённого пользователя.</returns>
        Task<UserEntity> UpdateAsync(UserEntity entity);
    }
}
