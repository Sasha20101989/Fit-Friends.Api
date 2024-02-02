using System.ComponentModel.DataAnnotations;

namespace FitFriends.ServiceLibrary.Enums
{
    /// <summary>
    /// Пречисление представляющее станцию
    /// </summary>
    public enum Station
    {
        /// <summary>
        /// Пионерская
        /// </summary>
        [Display(Name = "Pionerskaya")]
        Pionerskaya,
        /// <summary>
        /// Петроградская
        /// </summary>
        [Display(Name = "Petrogradskaya")]
        Petrogradskaya,
        /// <summary>
        /// Удельная
        /// </summary>
        [Display(Name = "Udelnaya")]
        Udelnaya,
        /// <summary>
        /// Звёздная
        /// </summary>
        [Display(Name = "Zvezdnaya")]
        Zvezdnaya,
        /// <summary>
        /// Спортивная
        /// </summary>
        [Display(Name = "Sportivnaya")]
        Sportivnaya
    }
}
