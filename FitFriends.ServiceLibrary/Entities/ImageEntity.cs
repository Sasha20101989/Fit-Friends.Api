using FitFriends.ServiceLibrary.Attributes;
using FitFriends.ServiceLibrary.Properties;
using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations.Schema;

namespace FitFriends.ServiceLibrary.Entities
{
    public class ImageEntity
    {
        public Guid Id { get; set; }

        [NotMapped]
        public IFormFile ImageFile { get; set; }

        [ImageValidation(ErrorMessageResourceType = typeof(ValidationMessages), ErrorMessageResourceName = "InvalidImageErrorMessage")]
        public string ImageTitle { get; set; }
    }
}
