using FitFriends.ServiceLibrary.Domains.Contracts;
using FitFriends.ServiceLibrary.Entities;
using FitFriends.ServiceLibrary.Repositories.Contracts;

namespace FitFriends.ServiceLibrary.Domains
{
    public class CertificateService : ICertificateService
    {
        private readonly ICertificateRepository _certificateRepository;

        public CertificateService(ICertificateRepository certificateRepository)
        {
            _certificateRepository = certificateRepository;
        }

        public async Task DeleteAsync(Guid certificateId)
        {
            await _certificateRepository.DeleteAsync(certificateId);
        }

        public async Task<IEnumerable<CertificateEntity>> GetAllByUserAsync(Guid userId)
        {
            return await _certificateRepository.GetAllByUserAsync(userId);
        }

        public async Task<CertificateEntity> GetByIdAsync(Guid certificateId)
        {
            return await _certificateRepository.GetByIdAsync(certificateId);
        }

        public async Task InsertAsync(CertificateEntity entity)
        {
            await _certificateRepository.InsertAsync(entity);
        }

        public async Task<CertificateEntity> UpdateAsync(CertificateEntity entity)
        {
            return await _certificateRepository.UpdateAsync(entity);
        }
    }
}
