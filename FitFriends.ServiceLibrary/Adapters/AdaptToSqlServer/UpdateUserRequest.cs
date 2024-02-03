﻿using FitFriends.ServiceLibrary.Entities;
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
            Password = entity.Password;
            AvatarId = entity.AvatarId;
            Gender = entity.Gender.ToFriendlyString();
            DateBirth = entity.DateBirth;
            Role = entity.Role.ToFriendlyString();
            Description = entity.Description;
            Station = entity.Station.ToFriendlyString();
            PageImageId = entity.PageImageId;
            LevelPreparation = entity.LevelPreparation.ToFriendlyString();
            IsReady = entity.IsReady;
        }

        public Guid UserId { get; set; }

        public Guid? AvatarId { get; set; }

        public Guid PageImageId { get; set; }

        public string Name { get; set; }

        public string Email { get; set; }

        public string Password { get; set; }

        public string Gender { get; set; }

        public DateTime? DateBirth { get; set; }

        public string Role { get; set; }

        public string? Description { get; set; }

        public string Station { get; set; }

        //public ImageEntity ImageForPage { get; set; }

        public string LevelPreparation { get; set; }

        public bool IsReady { get; set; }
       
    }
}
