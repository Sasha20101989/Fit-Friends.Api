using FitFriends.ServiceLibrary.Entities;

namespace FitFriends.ServiceLibrary.Domains.Contracts
{
    public interface IImageService
    {
        Task CreateAsync(ImageEntity entity);
        Task<ImageEntity> GetByIdAsync(Guid imageId);
        Task RemoveAsync(Guid imageId);
    }
}
