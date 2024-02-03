using FitFriends.ServiceLibrary.Domains.Contracts;
using FitFriends.ServiceLibrary.Entities;
using FitFriends.ServiceLibrary.Repositories.Contracts;

namespace FitFriends.ServiceLibrary.Domains
{
    public class CertificateService : ICertificateService
    {
        private readonly ICertificateRepository _certificateRepository;

        private readonly IImageService _imageService;

        public CertificateService(ICertificateRepository certificateRepository, IImageService imageService)
        {
            _certificateRepository = certificateRepository;

            _imageService = imageService;
        }

        public async Task DeleteAsync(Guid certificateId)
        {
            await _certificateRepository.DeleteAsync(certificateId);
        }

        public async Task<IEnumerable<CertificateEntity>> GetAllByUserAsync(Guid userId)
        {
            return await _certificateRepository.GetAllByUserAsync(userId);
        }

        public async Task<CertificateEntity?> GetByIdAsync(Guid certificateId)
        {
            CertificateEntity certificateEntity = await _certificateRepository.GetByIdAsync(certificateId);

            if (certificateEntity is not null)
            {
                if (certificateEntity.ImageId is not null)
                {
                    certificateEntity.Image = await _imageService.GetByIdAsync((Guid)certificateEntity.ImageId);
                }
            }

            return certificateEntity;
        }

        public async Task InsertAsync(CertificateEntity entity)
        {
            await _certificateRepository.InsertAsync(entity);
        }

        public async Task<CertificateEntity?> UpdateAsync(CertificateEntity entity)
        {
            Guid? oldImageId = entity.ImageId;

            await _imageService.CreateAsync(entity.Image);

            ImageEntity imageEntity = await _imageService.GetByIdAsync(entity.Image.Id);

            if (imageEntity is not null)
            {
                entity.ImageId = imageEntity.Id;

                CertificateEntity updatedCertificate = await _certificateRepository.UpdateAsync(entity);

                if (oldImageId is not null)
                {
                    await _imageService.RemoveAsync((Guid)oldImageId);
                }

                return updatedCertificate;
            }
            else
            {
                return null;
            }
        }
    }
}
