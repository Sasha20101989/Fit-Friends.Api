using FitFriends.ServiceLibrary.Entities;
using FitFriends.ServiceLibrary.Extensions.EnumExtensions;

namespace FitFriends.ServiceLibrary.Adapters.AdaptToSqlServer
{
    public class UpdateUserRequest
    {
        public UpdateUserRequest(UserEntity entity)
        {
            UserId = entity.UserId;
            Name = entity.Name;
            Email = entity.Email;
            Avatar = entity.Avatar;
            Password = entity.Password;
            Gender = entity.Gender.ToFriendlyString();
            DateBirth = entity.DateBirth;
            Role = entity.Role.ToFriendlyString();
            Description = entity.Description;
            Station = entity.Station.ToFriendlyString();
            ImageForPage = entity.ImageForPage;
            LevelPreparation = entity.LevelPreparation.ToFriendlyString();
            IsReady = entity.IsReady;
        }

        public Guid UserId { get; set; }

        public string Name { get; set; }

        public string Email { get; set; }

        public ImageEntity? Avatar { get; set; }

        public string Password { get; set; }

        public string Gender { get; set; }

        public DateTime? DateBirth { get; set; }

        public string Role { get; set; }

        public string? Description { get; set; }

        public string Station { get; set; }

        public ImageEntity ImageForPage { get; set; }

        public string LevelPreparation { get; set; }

        public bool IsReady { get; set; }
       
    }
}
