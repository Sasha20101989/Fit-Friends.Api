using FitFriends.ServiceLibrary.Domains.Contracts;
using FitFriends.ServiceLibrary.Entities;
using FitFriends.ServiceLibrary.Repositories.Contracts;

namespace FitFriends.ServiceLibrary.Domains
{
    public class ImageService : IImageService
    {
        private readonly IImageRepository _imageRepository;

        private readonly string _subDir = nameof(ImageEntity);

        public ImageService(IImageRepository imageRepository)
        {
            _imageRepository = imageRepository;
        }

        public async Task CreateAsync(ImageEntity entity)
        {
            entity.Id = Guid.NewGuid();

            await _imageRepository.CreateAsync(entity);
        }

        public async Task<ImageEntity?> UpdateImageAsync(ImageEntity entity)
        {
            await CreateAsync(entity);

            return await GetByIdAsync(entity.Id);
        }

        public async Task<ImageEntity> GetByIdAsync(Guid imageId)
        {
            return await _imageRepository.GetByIdAsync(imageId);
        }

        public async Task RemoveImageAndDirectoryAsync(string subDirName, Guid? imageId, Guid id, string wwwrootPath)
        {
            if (imageId is not null)
            {
                string subDirPath = $"{_subDir}{id}";
                string directoryPath = Path.Combine(wwwrootPath, subDirName, subDirPath);

                DeleteDirectory(directoryPath);

                await _imageRepository.RemoveAsync((Guid)imageId);
            }
        }

        public void RemoveImageFromDirectory(string subDirName, Guid? imageId, string imageTitle, Guid id, string wwwrootPath)
        {
            string subDirPath = $"{_subDir}{id}";

            if (imageId is not null)
            {
                string filePath = Path.Combine(wwwrootPath, subDirName, subDirPath, imageTitle);
                
                if (File.Exists(filePath))
                {
                    File.Delete(filePath);
                }
            }
        }

        public async Task RemoveImageFromDbAsync(Guid? imageId)
        {
            if (imageId is not null)
            {
                await RemoveAsync((Guid)imageId);
            }
        }

        private void DeleteDirectory(string directoryPath)
        {
            if (Directory.Exists(directoryPath))
            {
                Directory.Delete(directoryPath, true);
            }
        }

        private async Task RemoveAsync(Guid imageId)
        {
            await _imageRepository.RemoveAsync(imageId);
        }
    }
}
