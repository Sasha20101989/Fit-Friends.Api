using System.ComponentModel.DataAnnotations;

namespace FitFriends.ServiceLibrary.Enums
{
    /// <summary>
    /// Перечисление представляющее тип тренировок
    /// </summary>
    public enum TypeTraining
    {
        /// <summary>
        /// йога
        /// </summary>
        [Display(Name = "Yoga")]
        Yoga,
        /// <summary>
        /// бег
        /// </summary>
        [Display(Name = "Running")]
        Running,
        /// <summary>
        /// бокс
        /// </summary>
        [Display(Name = "Boxing")]
        Boxing,
        /// <summary>
        /// стрейчинг
        /// </summary>
        [Display(Name = "Stretching")]
        Stretching,
        /// <summary>
        /// кроссфит
        /// </summary>
        [Display(Name = "Crossfit")]
        Crossfit,
        /// <summary>
        /// аэробика
        /// </summary>
        [Display(Name = "Aerobics")]
        Aerobics,
        /// <summary>
        /// пилатес
        /// </summary>
        [Display(Name = "Pilates")]
        Pilates
    }
}
