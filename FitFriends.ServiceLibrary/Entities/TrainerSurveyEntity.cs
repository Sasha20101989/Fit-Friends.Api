using FitFriends.ServiceLibrary.Properties;
using System.ComponentModel.DataAnnotations;

namespace FitFriends.ServiceLibrary.Entities
{
    /// <summary>
    /// Класс представляет собой информацию об опросе тренера.
    /// </summary>
    public class TrainerSurveyEntity
    {
        /// <summary>
        /// Уникальный идентификатор пользователя.
        /// </summary>
        [Key]
        public Guid UserId { get; set; }

        /// <summary>
        /// Сертификаты тренера.
        /// </summary>
        [Required(ErrorMessageResourceType = typeof(ValidationMessages), ErrorMessageResourceName = "RequiredErrorMessage")]
        public IList<CertificateEntity> Certificates { get; set; } = new List<CertificateEntity>();

        /// <summary>
        /// Достижения тренера.
        /// </summary>
        [MinLength(10, ErrorMessageResourceType = typeof(ValidationMessages), ErrorMessageResourceName = "MinLengthErrorMessage")]
        [MaxLength(140, ErrorMessageResourceType = typeof(ValidationMessages), ErrorMessageResourceName = "MaxLengthErrorMessage")]
        public string? TrainerAchievements { get; set; }
    }
}
