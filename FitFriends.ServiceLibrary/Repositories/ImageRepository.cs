using FitFriends.ServiceLibrary.DataAccess.Contracts;
using FitFriends.ServiceLibrary.Entities;
using FitFriends.ServiceLibrary.Enums;
using FitFriends.ServiceLibrary.Repositories.Contracts;
using static Dapper.SqlMapper;

namespace FitFriends.ServiceLibrary.Repositories
{
    /// <summary>
    /// Репозиторий сертификатов.
    /// </summary>
    public class ImageRepository : IImageRepository
    {
        private ISqlDataAccess _sqlDataAccess;

        /// <summary>
        /// Инициализирует новый экземпляр класса <see cref="ImageRepository"/>.
        /// </summary>
        /// <param name="sqlDataAccess">Объект доступа к данным SQL.</param>
        public ImageRepository(ISqlDataAccess sqlDataAccess)
        {
            _sqlDataAccess = sqlDataAccess;
        }

        /// <inheritdoc />
        public async Task CreateAsync(ImageEntity entity)
        {
            await _sqlDataAccess.SaveData(
                StoredProcedureImage.CreateImage,
                new { entity.Id, entity.ImageTitle}
            );
        }

        public async Task<ImageEntity> GetByIdAsync(Guid imageId)
        {
            IEnumerable<ImageEntity> result = await _sqlDataAccess.LoadData<ImageEntity>(
               StoredProcedureImage.GetImageById,
               new { Id = imageId }
            );

            return result.FirstOrDefault();
        }

        public async Task RemoveAsync(Guid imageId)
        {
            await _sqlDataAccess.SaveData(
                StoredProcedureImage.RemoveImage,
                new { Id = imageId }
            );
        }
    }
}
