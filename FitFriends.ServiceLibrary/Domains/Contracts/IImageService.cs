using FitFriends.ServiceLibrary.Entities;

namespace FitFriends.ServiceLibrary.Domains.Contracts
{
    public interface IImageService
    {
        Task CreateAsync(ImageEntity entity);
        
        Task<ImageEntity> GetByIdAsync(Guid imageId);

        Task<ImageEntity?> UpdateImageAsync(ImageEntity entity);

        Task RemoveImageFromDbAsync(Guid? imageId);

        Task RemoveImageAndDirectoryAsync(string subDirName, Guid? imageId, Guid id, string wwwrootPath);

        void RemoveImageFromDirectory(string subDirName, Guid? imageId, string imageTitle, Guid id, string wwwrootPath);
    }
}
