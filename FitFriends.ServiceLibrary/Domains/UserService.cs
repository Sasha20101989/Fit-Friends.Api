using FitFriends.ServiceLibrary.Domains.Contracts;
using FitFriends.ServiceLibrary.Entities;
using FitFriends.ServiceLibrary.QueryParameters;
using FitFriends.ServiceLibrary.Repositories.Contracts;

namespace FitFriends.ServiceLibrary.Domains
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;

        public UserService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task DeleteAsync(Guid userId)
        {
            await _userRepository.DeleteAsync(userId);
        }

        public async Task<IEnumerable<UserEntity>> GetAllAsync(PaginationParameters? pagination)
        {
            if (pagination is null)
            {
                return [];
            }

            var pageSize = Math.Min(pagination.PageSize, 50);

            int offset = (pagination.PageNumber - 1) * pageSize;

            return await _userRepository.GetAllAsync(pageSize, offset);
        }

        public async Task<UserEntity> GetByIdAsync(Guid userId)
        {
            return await _userRepository.GetByIdAsync(userId);
        }

        public async Task InsertAsync(UserEntity userEntity)
        {
            userEntity.UserId = Guid.NewGuid();

            await _userRepository.InsertAsync(userEntity);
        }

        public async Task<UserEntity> UpdateAsync(UserEntity user)
        {
            return await _userRepository.UpdateAsync(user);
        }
    }
}
