using FitFriends.ServiceLibrary.Attributes;
using FitFriends.ServiceLibrary.Properties;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

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
        /// Уникальный идентификатор изображения.
        /// </summary>
        public Guid? ImageId { get; set; }

        /// <summary>
        /// Название сертификата.
        /// </summary>
        [Required(ErrorMessageResourceType = typeof(ValidationMessages), ErrorMessageResourceName = "RequiredErrorMessage")]
        public string CertificateName { get; set; }

        /// <summary>
        /// Изображение сертификата.
        /// </summary>
        public virtual ImageEntity? Image { get; set; }
    }
}
