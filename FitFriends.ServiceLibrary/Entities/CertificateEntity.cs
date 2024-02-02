using FitFriends.ServiceLibrary.Attributes;
using FitFriends.ServiceLibrary.Properties;
using System.ComponentModel.DataAnnotations;

namespace FitFriends.ServiceLibrary.Entities
{
    /// <summary>
    /// Класс представляет собой информацию о сертификате.
    /// </summary>
    public class CertificateEntity
    {
        /// <summary>
        /// Уникальный идентификатор сертификата.
        /// </summary>
        [Key]
        public Guid CertificateId { get; set; }

        /// <summary>
        /// Уникальный идентификатор пользователя, которому принадлежит сертификат.
        /// </summary>
        public Guid UserId { get; set; }

        /// <summary>
        /// Название сертификата.
        /// </summary>
        [Required(ErrorMessageResourceType = typeof(ValidationMessages), ErrorMessageResourceName = "RequiredErrorMessage")]
        [ImageValidation(ErrorMessageResourceType = typeof(ValidationMessages), ErrorMessageResourceName = "InvalidImageErrorMessage")]
        public string CertificateName { get; set; }

        /// <summary>
        /// Изображение сертификата.
        /// </summary>
        public string? Image { get; set; }
    }

}
