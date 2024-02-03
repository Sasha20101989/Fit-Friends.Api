using FitFriends.ServiceLibrary.Entities;

namespace FitFriends.ServiceLibrary.Repositories.Contracts
{
    public interface IImageRepository
    {
        Task CreateAsync(ImageEntity entity);

        Task<ImageEntity> GetByIdAsync(Guid imageId);
        Task RemoveAsync(Guid imageId);
    }
}
