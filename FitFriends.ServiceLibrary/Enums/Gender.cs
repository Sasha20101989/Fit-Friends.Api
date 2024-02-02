using System.ComponentModel.DataAnnotations;

namespace FitFriends.ServiceLibrary.Enums
{
    /// <summary>
    /// Перечисление представляющее пол
    /// </summary>
    public enum Gender
    {
        /// <summary>
        /// женский
        /// </summary>
        [Display(Name = "Female")]
        Female,
        /// <summary>
        /// мужской 
        /// </summary>
        [Display(Name = "Male")]
        Male,
        /// <summary>
        /// неважно
        /// </summary>
        [Display(Name = "Unimportant")]
        Unimportant
    }
}
