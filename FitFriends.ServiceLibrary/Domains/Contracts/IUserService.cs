using FitFriends.ServiceLibrary.Entities;
using FitFriends.ServiceLibrary.QueryParameters;

namespace FitFriends.ServiceLibrary.Domains.Contracts
{
    public interface IUserService
    {
        Task<IEnumerable<UserEntity>> GetAllAsync(PaginationParameters? pagination);

        Task<UserEntity> GetByIdAsync(Guid userId);

        Task InsertAsync(UserEntity userEntity);

        Task<UserEntity> UpdateAsync(UserEntity user);

        Task DeleteAsync(Guid userId);
    }
}
