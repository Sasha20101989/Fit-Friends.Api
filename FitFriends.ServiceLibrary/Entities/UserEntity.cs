using FitFriends.ServiceLibrary.Attributes;
using FitFriends.ServiceLibrary.Enums;
using FitFriends.ServiceLibrary.Properties;
using System.ComponentModel.DataAnnotations;

namespace FitFriends.ServiceLibrary.Entities
{
    /// <summary>
    /// Класс представляющий пользователя.
    /// </summary>
    public class UserEntity
    {
        /// <summary>
        /// Уникальный идентификатор пользователя.
        /// </summary>
        [Key]
        public Guid UserId { get; set; }

        /// <summary>
        /// Имя пользователя.
        /// </summary>
        [Required(ErrorMessageResourceType = typeof(ValidationMessages), ErrorMessageResourceName = "RequiredErrorMessage")]
        [MinLength(1, ErrorMessageResourceType = typeof(ValidationMessages), ErrorMessageResourceName = "MinLengthErrorMessage")]
        [MaxLength(15, ErrorMessageResourceType = typeof(ValidationMessages), ErrorMessageResourceName = "MaxLengthErrorMessage")]
        public string Name { get; set; }

        /// <summary>
        /// Адрес электронной почты пользователя.
        /// </summary>
        [Required(ErrorMessageResourceType = typeof(ValidationMessages), ErrorMessageResourceName = "RequiredErrorMessage")]
        [EmailAddress(ErrorMessageResourceType = typeof(ValidationMessages), ErrorMessageResourceName = "InvalidEmailErrorMessage")]
        public string Email { get; set; }

        /// <summary>
        /// Аватар пользователя.
        /// </summary>
        public ImageEntity? Avatar { get; set; }

        /// <summary>
        /// Пароль пользователя.
        /// </summary>
        [Required(ErrorMessageResourceType = typeof(ValidationMessages), ErrorMessageResourceName = "RequiredErrorMessage")]
        [MinLength(6, ErrorMessageResourceType = typeof(ValidationMessages), ErrorMessageResourceName = "MinLengthErrorMessage")]
        [MaxLength(12, ErrorMessageResourceType = typeof(ValidationMessages), ErrorMessageResourceName = "MaxLengthErrorMessage")]
        public string Password { get; set; }

        /// <summary>
        /// Пол пользователя.
        /// </summary>
        public Gender Gender { get; set; }

        /// <summary>
        /// Дата рождения пользователя.
        /// </summary>
        public DateTime? DateBirth { get; set; }

        /// <summary>
        /// Роль пользователя.
        /// </summary>
        public Role Role { get; set; }

        /// <summary>
        /// Описание пользователя.
        /// </summary>
        [MinLength(10, ErrorMessageResourceType = typeof(ValidationMessages), ErrorMessageResourceName = "MinLengthErrorMessage")]
        [MaxLength(140, ErrorMessageResourceType = typeof(ValidationMessages), ErrorMessageResourceName = "MaxLengthErrorMessage")]
        public string? Description { get; set; }

        /// <summary>
        /// Станция пользователя.
        /// </summary>
        public Station Station { get; set; }

        /// <summary>
        /// Изображение для страницы пользователя.
        /// </summary>
        [Required(ErrorMessageResourceType = typeof(ValidationMessages), ErrorMessageResourceName = "RequiredErrorMessage")]
        [ImageValidation(ErrorMessageResourceType = typeof(ValidationMessages), ErrorMessageResourceName = "InvalidImageErrorMessage")]
        public ImageEntity ImageForPage { get; set; }

        /// <summary>
        /// Дата создания пользователя.
        /// </summary>
        public DateTime CreatedAt { get; set; }

        /// <summary>
        /// Уровень подготовки пользователя.
        /// </summary>
        [Required(ErrorMessageResourceType = typeof(ValidationMessages), ErrorMessageResourceName = "RequiredErrorMessage")]
        public LevelPreparation LevelPreparation { get; set; }

        /// <summary>
        /// Флаг готовности пользователя.
        /// </summary>
        public bool IsReady { get; set; }

        /// <summary>
        /// Список видов тренировок пользователя с ограничением по количеству.
        /// </summary>
        [LimitCount(1, 3, ErrorMessageResourceType = typeof(ValidationMessages), ErrorMessageResourceName = "LimitErrorMessage")]
        public virtual IList<TypeTraining> TrainingTypes { get; set; }
    }
}