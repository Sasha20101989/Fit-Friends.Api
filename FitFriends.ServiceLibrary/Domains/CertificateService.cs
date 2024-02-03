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
                    certificateEntity.CertificateImage = await _imageService.GetByIdAsync((Guid)certificateEntity.ImageId);
                }
            }

            return certificateEntity;
        }

        public async Task InsertAsync(CertificateEntity entity)
        {
            await _certificateRepository.InsertAsync(entity);
        }

        public async Task<CertificateEntity?> UpdateCertificateAsync(CertificateEntity entity)
        {
            return await _certificateRepository.UpdateAsync(entity);
        }

        public async Task<CertificateEntity?> UpdateCertificateWithNewImageAsync(Guid certificateId, ImageEntity imageEntity, string wwwrootPath)
        {
            CertificateEntity? certificateEntity = await GetByIdAsync(certificateId);

            if (certificateEntity is null)
            {
                return null;
            }

            Guid? oldImageId = certificateEntity.ImageId;
            string? oldCertificateImageTitle = certificateEntity.CertificateImage?.ImageTitle;

            certificateEntity.CertificateImage = imageEntity;

            ImageEntity? updatedImageEntity = await UpdateImageAsync(certificateEntity.CertificateImage);

            if (updatedImageEntity is not null)
            {
                certificateEntity.ImageId = updatedImageEntity.Id;

                CertificateEntity? updatedCertificate = await UpdateCertificateAsync(certificateEntity);

                if (updatedCertificate is not null)
                {
                    if (oldImageId is not null)
                    {
                        if (oldCertificateImageTitle != certificateEntity.CertificateImage.ImageTitle)
                        {
                            _imageService.RemoveImageFromDirectory("Certificates", (Guid)oldImageId, oldCertificateImageTitle, updatedCertificate.CertificateId, wwwrootPath);
                        }

                        await _imageService.RemoveImageFromDbAsync(oldImageId);
                    }

                    return updatedCertificate;
                }           
            }

            return null;
        }

        private async Task<ImageEntity?> UpdateImageAsync(ImageEntity imageEntity)
        {
            await _imageService.CreateAsync(imageEntity);

            return await _imageService.GetByIdAsync(imageEntity.Id);
        }
    }
}
