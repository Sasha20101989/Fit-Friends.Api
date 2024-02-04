using FitFriends.ServiceLibrary.Entities;
using FitFriends.ServiceLibrary.QueryParameters;

namespace FitFriends.ServiceLibrary.Domains.Contracts
{
    public interface IUserService
    {
        Task<IEnumerable<UserEntity>> GetAllAsync(PaginationParameters? pagination);

        Task<UserEntity?> GetByIdAsync(Guid userId);

        Task InsertAsync(UserEntity userEntity);

        Task<UserEntity?> UpdateUserWithNewAvatarAsync(Guid userId, ImageEntity imageEntity, string wwwrootPath, string subDirectory);

        Task<UserEntity?> UpdateUserWithNewPageImageAsync(Guid userId, ImageEntity imageEntity, string wwwrootPath, string subDirectory);

        Task<UserEntity?> UpdateUserAsync(UserEntity entity);

        Task DeleteAsync(Guid userId);


        //TODO: Реализовать универсальный дженерик для загрузки изображения
        //Task<UserEntity?> UpdateUserWithNewImageAsync(
        //    Guid userId,
        //    ImageEntity imageEntity,
        //    string wwwrootPath,
        //    Func<UserEntity, ImageEntity?> getImage,
        //    Action<UserEntity, ImageEntity> setImage,
        //    Func<UserEntity, Task<UserEntity?>> updateUser);
    }
}
