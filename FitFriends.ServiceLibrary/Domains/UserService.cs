using FitFriends.ServiceLibrary.Domains.Contracts;
using FitFriends.ServiceLibrary.Entities;
using FitFriends.ServiceLibrary.QueryParameters;
using FitFriends.ServiceLibrary.Repositories.Contracts;

namespace FitFriends.ServiceLibrary.Domains
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;

        private readonly IImageService _imageService;

        public UserService(IUserRepository userRepository, IImageService imageService)
        {
            _userRepository = userRepository;

            _imageService = imageService;
        }

        public async Task DeleteAsync(Guid userId)
        {
            await _userRepository.DeleteAsync(userId);
        }

        public async Task<UserEntity?> FindByEmailAsync(string email)
        {
            return await _userRepository.GetByEmailAsync(email);
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

        public async Task<UserEntity?> GetByIdAsync(Guid userId)
        {
            UserEntity userEntity = await _userRepository.GetByIdAsync(userId);

            if (userEntity is not null)
            {
                if (userEntity.AvatarId is not null)
                {
                    userEntity.Avatar = await _imageService.GetByIdAsync((Guid)userEntity.AvatarId);
                }

                userEntity.PageImage = await _imageService.GetByIdAsync(userEntity.PageImageId);
            }

            return userEntity;
        }

        public async Task InsertAsync(UserEntity userEntity)
        {
            userEntity.UserId = Guid.NewGuid();

            await _userRepository.InsertAsync(userEntity);
        }

        public async Task<UserEntity?> UpdateUserAsync(UserEntity entity)
        {
            return await _userRepository.UpdateAsync(entity);
        }

        public async Task<UserEntity?> UpdateUserWithNewAvatarAsync(Guid userId, ImageEntity imageEntity, string wwwrootPath, string subDirectory)
        {
            UserEntity? userEntity = await GetByIdAsync(userId);

            if (userEntity is null)
            {
                return null;
            }

            Guid? oldAvatarImageId = userEntity.AvatarId;

            string? oldAvatarImageTitle = userEntity.Avatar?.ImageTitle;

            userEntity.Avatar = imageEntity;

            ImageEntity? updatedAvatarEntity = await _imageService.UpdateImageAsync(userEntity.Avatar);

            if (updatedAvatarEntity is not null)
            {
                userEntity.AvatarId = updatedAvatarEntity.Id;

                UserEntity? updatedUser = await UpdateUserAsync(userEntity);

                if (updatedUser is not null)
                {
                    if (oldAvatarImageId is not null)
                    {
                        if (oldAvatarImageTitle != userEntity.Avatar.ImageTitle)
                        {
                            _imageService.RemoveImageFromDirectory(subDirectory, (Guid)oldAvatarImageId, oldAvatarImageTitle, updatedUser.UserId, wwwrootPath);
                        }

                        await _imageService.RemoveImageFromDbAsync(oldAvatarImageId);
                    }

                    return updatedUser;
                }
            }

            return null;
        }

        public async Task<UserEntity?> UpdateUserWithNewPageImageAsync(Guid userId, ImageEntity imageEntity, string wwwrootPath, string subDirectory)
        {
            UserEntity? userEntity = await GetByIdAsync(userId);

            if (userEntity is null)
            {
                return null;
            }

            Guid oldPageImageId = userEntity.PageImageId;
            string? oldPageImageTitle = userEntity.PageImage?.ImageTitle;

            userEntity.PageImage = imageEntity;

            ImageEntity? updatedPageImageEntity = await _imageService.UpdateImageAsync(userEntity.PageImage);

            if (updatedPageImageEntity is not null)
            {
                userEntity.PageImageId = updatedPageImageEntity.Id;

                UserEntity? updatedUser = await UpdateUserAsync(userEntity);

                if (updatedUser is not null)
                {
                    if (oldPageImageId != Guid.Empty)
                    {
                        if (oldPageImageTitle != userEntity.PageImage.ImageTitle)
                        {
                            _imageService.RemoveImageFromDirectory(subDirectory, oldPageImageId, oldPageImageTitle, updatedUser.UserId, wwwrootPath);
                        }

                        await _imageService.RemoveImageFromDbAsync(oldPageImageId);
                    }

                    return updatedUser;
                }
            }

            return null;
        }
    }
}
