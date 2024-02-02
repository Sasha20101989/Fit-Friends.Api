using System.ComponentModel.DataAnnotations;

namespace FitFriends.ServiceLibrary.Attributes
{
    /// <summary>
    /// Атрибут валидации изображения.
    /// </summary>
    [AttributeUsage(AttributeTargets.Property | AttributeTargets.Field, AllowMultiple = false)]
    public class ImageValidationAttribute : ValidationAttribute
    {
        private readonly string[] _allowedExtensions = [".jpg", ".jpeg", ".png"];

        /// <summary>
        /// Проверяет, что значение свойства (путь к файлу изображения) имеет одно из разрешенных расширений (.jpg, .png).
        /// </summary>
        /// <param name="value">Значение свойства.</param>
        /// <returns>True, если значение прошло валидацию, в противном случае - false.</returns>
        public override bool IsValid(object value)
        {
            if (value is string filePath && !string.IsNullOrEmpty(filePath))
            {
                string extension = Path.GetExtension(filePath);

                if (string.IsNullOrEmpty(extension))
                {
                    return false;
                }

                if (!_allowedExtensions.Contains(extension, StringComparer.OrdinalIgnoreCase))
                {
                    return false;
                }

                return true;
            }

            // Значение не является строкой или пустым/нулевым
            return false;
        }
    }
}
