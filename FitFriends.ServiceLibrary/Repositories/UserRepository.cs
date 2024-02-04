using FitFriends.ServiceLibrary.Adapters.AdaptToSqlServer;
using FitFriends.ServiceLibrary.DataAccess.Contracts;
using FitFriends.ServiceLibrary.Entities;
using FitFriends.ServiceLibrary.Enums;
using FitFriends.ServiceLibrary.Repositories.Contracts;

namespace FitFriends.ServiceLibrary.Repositories
{
    /// <summary>
    /// Репозиторий пользователей.
    /// </summary>
    public class UserRepository : IUserRepository
    {
        private ISqlDataAccess _sqlDataAccess;

        /// <summary>
        /// Инициализирует новый экземпляр класса <see cref="UserRepository"/>.
        /// </summary>
        /// <param name="sqlDataAccess">Объект доступа к данным SQL.</param>
        public UserRepository(ISqlDataAccess sqlDataAccess)
        {
            _sqlDataAccess = sqlDataAccess;
        }

        /// <inheritdoc />
        public async Task DeleteAsync(Guid userId)
        {
            await _sqlDataAccess.SaveData(
                StoredProcedureUser.DeleteUser,
                new { UserId = userId }
            );
        }

        /// <inheritdoc />
        public async Task<IEnumerable<UserEntity>> GetAllAsync(int pageSize, int offset)
        {
            IEnumerable<UserEntity> result = await _sqlDataAccess.LoadData<UserEntity>(
                StoredProcedureUser.GetAllUsers,
                new { Offset = offset, PageSize = pageSize }
              );

            return result;
        }

        /// <inheritdoc />
        public async Task<UserEntity?> GetByEmailAsync(string email)
        {
            IEnumerable<UserEntity> result = await _sqlDataAccess.LoadData<UserEntity>(
                StoredProcedureUser.GetUserByEmail,
                new { Email = email }
              );

            return result.FirstOrDefault();
        }

        /// <inheritdoc />
        public async Task<UserEntity?> GetByIdAsync(Guid userId)
        {
            IEnumerable<UserEntity> result = await _sqlDataAccess.LoadData<UserEntity>(
                StoredProcedureUser.GetUserById,
                new { UserId = userId }
             );

            return result.FirstOrDefault();
        }

        /// <inheritdoc />
        public async Task InsertAsync(UserEntity entity)
        {
            CreateUserRequest request = new(entity);

            await _sqlDataAccess.SaveData(
                StoredProcedureUser.InsertUser,
                request
            );
        }

        /// <inheritdoc />
        public async Task<UserEntity?> UpdateAsync(UserEntity entity)
        {
           UpdateUserRequest request = new(entity);

            await _sqlDataAccess.SaveData(
               StoredProcedureUser.UpdateUser,
                request
            );

            return await GetByIdAsync(entity.UserId);
        }
    }
}
