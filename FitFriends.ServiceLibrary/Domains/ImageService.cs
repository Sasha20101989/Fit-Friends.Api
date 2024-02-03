using FitFriends.ServiceLibrary.Domains.Contracts;
using FitFriends.ServiceLibrary.Entities;
using FitFriends.ServiceLibrary.Repositories.Contracts;

namespace FitFriends.ServiceLibrary.Domains
{
    public class ImageService : IImageService
    {
        private readonly IImageRepository _imageRepository;

        public ImageService(IImageRepository imageRepository)
        {
            _imageRepository = imageRepository;
        }

        public async Task CreateAsync(ImageEntity entity)
        {
            entity.Id = Guid.NewGuid();

            await _imageRepository.CreateAsync(entity);
        }

        public async Task<ImageEntity> GetByIdAsync(Guid imageId)
        {
            return await _imageRepository.GetByIdAsync(imageId);
        }

        public async Task RemoveAsync(Guid imageId)
        {
            await _imageRepository.RemoveAsync(imageId);
        }
    }
}
