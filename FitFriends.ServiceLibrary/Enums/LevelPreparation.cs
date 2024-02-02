using System.ComponentModel.DataAnnotations;

namespace FitFriends.ServiceLibrary.Enums
{
    /// <summary>
    /// Перечисление представляющее уровень подготовки
    /// </summary>
    public enum LevelPreparation
    {
        /// <summary>
        /// новичок
        /// </summary>
        [Display(Name = "Beginner")]
        Beginner,
        /// <summary>
        /// любитель
        /// </summary>
        [Display(Name = "Amateur")]
        Amateur,
        /// <summary>
        /// профессионал
        /// </summary>
        [Display(Name = "Professional")]
        Professional
    }
}
