using FitFriends.ServiceLibrary.Adapters.AdaptToSqlServer;
using FitFriends.ServiceLibrary.DataAccess.Contracts;
using FitFriends.ServiceLibrary.Entities;
using FitFriends.ServiceLibrary.Enums;
using FitFriends.ServiceLibrary.Repositories.Contracts;

namespace FitFriends.ServiceLibrary.Repositories
{
    /// <summary>
    /// Репозиторий сертификатов.
    /// </summary>
    public class CertificateRepository : ICertificateRepository
    {
        private ISqlDataAccess _sqlDataAccess;

        /// <summary>
        /// Инициализирует новый экземпляр класса <see cref="CertificateRepository"/>.
        /// </summary>
        /// <param name="sqlDataAccess">Объект доступа к данным SQL.</param>
        public CertificateRepository(ISqlDataAccess sqlDataAccess)
        {
            _sqlDataAccess = sqlDataAccess;
        }

        /// <inheritdoc />
        public async Task DeleteAsync(Guid certificateId)
        {
            await _sqlDataAccess.SaveData(
                StoredProcedureCertificate.DeleteCertificate,
                new { CertificateId = certificateId }
            );
        }

        /// <inheritdoc />
        public async Task<IEnumerable<CertificateEntity>> GetAllByUserAsync(Guid userId)
        {
            IEnumerable<CertificateEntity> result = await _sqlDataAccess.LoadData<CertificateEntity>(
                StoredProcedureCertificate.GetAllCertificatesByUser,
                new { UserId = userId }
              );

            return result;
        }

        /// <inheritdoc />
        public async Task<CertificateEntity> GetByIdAsync(Guid certificateId)
        {
            IEnumerable<CertificateEntity> result = await _sqlDataAccess.LoadData<CertificateEntity>(
               StoredProcedureCertificate.GetCertificateById,
               new { CertificateId = certificateId }
            );

            return result.First();
        }

        /// <inheritdoc />
        public async Task InsertAsync(CertificateEntity entity)
        {
            CreateCertificateRequest request = new(entity);

            await _sqlDataAccess.SaveData(
                StoredProcedureCertificate.InsertCertificate,
                request
            );
        }

        /// <inheritdoc />
        public async Task<CertificateEntity> UpdateAsync(CertificateEntity entity)
        {
            UpdateCertificateRequest request = new(entity);

            await _sqlDataAccess.SaveData(
               StoredProcedureCertificate.UpdateCertificate,
                request
            );

            return await GetByIdAsync(entity.CertificateId);
        }
    }
}
