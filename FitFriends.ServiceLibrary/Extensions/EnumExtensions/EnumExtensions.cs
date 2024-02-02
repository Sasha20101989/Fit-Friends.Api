using System.ComponentModel.DataAnnotations;
using System.Reflection;

namespace FitFriends.ServiceLibrary.Extensions.EnumExtensions
{
    /// <summary>
    /// Расширения для перечислений.
    /// </summary>
    public static class EnumExtensions
    {
        /// <summary>
        /// Получить отображаемую строку для значения перечисления с использованием атрибута DisplayAttribute.
        /// </summary>
        /// <param name="value">Значение перечисления.</param>
        /// <returns>Отображаемая строка.</returns>
        public static string? ToFriendlyString(this Enum value)
        {
            Type enumType = value.GetType();
            string? enumName = Enum.GetName(enumType, value);

            MemberInfo[] memberInfo = enumType.GetMember(enumName);

            return memberInfo.FirstOrDefault()?.GetCustomAttributes(typeof(DisplayAttribute), false)
                .FirstOrDefault() is DisplayAttribute displayAttribute ? displayAttribute.Name : enumName;
        }
    }
}
