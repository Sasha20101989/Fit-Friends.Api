using System.ComponentModel.DataAnnotations;

namespace FitFriends.ServiceLibrary.Enums
{
    /// <summary>
    /// Перечисление представляющее интервалы тренировок
    /// </summary>
    public enum TimeForTraining
    {
        /// <summary>
        /// 10-30 мин
        /// </summary>
        [Display(Name = "Short")]
        Short,
        /// <summary>
        /// 30-50 мин
        /// </summary>
        [Display(Name = "Medium")]
        Medium,
        /// <summary>
        /// 50-80 мин
        /// </summary>
        [Display(Name = "Long")]
        Long,
        /// <summary>
        /// 80-100 мин
        /// </summary>'
        [Display(Name = "ExtraLong")]
        ExtraLong,
    }
}
