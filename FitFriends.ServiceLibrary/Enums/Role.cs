using System.ComponentModel.DataAnnotations;

namespace FitFriends.ServiceLibrary.Enums
{
    /// <summary>
    /// Перечисление представляющее роль
    /// </summary>
    public enum Role
    {
        /// <summary>
        /// пользователь
        /// </summary>
        [Display(Name = "User")]
        User,
        /// <summary>
        /// тренер
        /// </summary>
        [Display(Name = "Trainer")]
        Trainer
    }
}
