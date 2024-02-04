using FitFriends.ServiceLibrary.Enums;
using FitFriends.ServiceLibrary.Properties;
using System.ComponentModel.DataAnnotations;

namespace FitFriends.ServiceLibrary.Entities
{
    /// <summary>
    /// Класс представляет собой информацию о пользовательском опросе.
    /// </summary>
    public class UserSurveyEntity
    {
        /// <summary>
        /// Уникальный идентификатор пользователя.
        /// </summary>
        [Key]
        public Guid UserId { get; set; }

        /// <summary>
        /// Время, выделенное на тренировки.
        /// </summary>
        [Required(ErrorMessageResourceType = typeof(ValidationMessages), ErrorMessageResourceName = "RequiredErrorMessage")]
        public TimeForTraining TimeForTraining { get; set; }

        /// <summary>
        /// Количество калорий для похудения пользователя.
        /// </summary>
        [Required(ErrorMessageResourceType = typeof(ValidationMessages), ErrorMessageResourceName = "RequiredErrorMessage")]
        [Range(1000, 5000, ErrorMessageResourceType = typeof(ValidationMessages), ErrorMessageResourceName = "RangeErrorMessage")]
        public int CaloriesToLose { get; set; }

        /// <summary>
        /// Количество калорий, которые пользователь должен сжигать ежедневно.
        /// </summary>
        [Required(ErrorMessageResourceType = typeof(ValidationMessages), ErrorMessageResourceName = "RequiredErrorMessage")]
        [Range(1000, 5000, ErrorMessageResourceType = typeof(ValidationMessages), ErrorMessageResourceName = "RangeErrorMessage")]
        public int DailyCaloriesToBurn { get; set; }
    }
}
