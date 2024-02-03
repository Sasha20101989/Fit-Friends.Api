using FitFriends.ServiceLibrary.Entities;
using FitFriends.ServiceLibrary.QueryParameters;

namespace FitFriends.ServiceLibrary.Domains.Contracts
{
    public interface IUserService
    {
        Task<IEnumerable<UserEntity>> GetAllAsync(PaginationParameters? pagination);

        Task<UserEntity?> GetByIdAsync(Guid userId);

        Task InsertAsync(UserEntity userEntity);

        Task<UserEntity?> UpdateUserWithNewAvatarAsync(Guid userId, ImageEntity imageEntity, string wwwrootPath);

        Task<UserEntity?> UpdateUserWithNewPageImageAsync(Guid userId, ImageEntity imageEntity, string wwwrootPath);

        Task<UserEntity?> UpdateUserAsync(UserEntity entity);

        Task DeleteAsync(Guid userId);
    }
}
