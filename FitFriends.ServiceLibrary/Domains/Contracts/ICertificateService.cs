using FitFriends.ServiceLibrary.Entities;

namespace FitFriends.ServiceLibrary.Domains.Contracts
{
    public interface ICertificateService
    {
        Task DeleteAsync(Guid certificateId);

        Task<IEnumerable<CertificateEntity>> GetAllByUserAsync(Guid userId);

        Task<CertificateEntity> GetByIdAsync(Guid certificateId);

        Task InsertAsync(CertificateEntity entity);

        Task<CertificateEntity> UpdateAsync(CertificateEntity entity);
    }
}
