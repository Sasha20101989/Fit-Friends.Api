using FitFriends.ServiceLibrary.Domains;
using FitFriends.ServiceLibrary.Domains.Contracts;
using FitFriends.ServiceLibrary.Entities;
using FitFriends.ServiceLibrary.Enums;
using Microsoft.AspNetCore.Mvc;

namespace FitFriends.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CertificateController : ControllerBase
    {
        private readonly IImageService _imageService;

        private readonly string _wwwrootPath;

        private readonly string _subDirectoryCertificates = SubDirectory.Certificates.ToString();

        public CertificateController(IWebHostEnvironment env, IImageService imageService)
        {
            _wwwrootPath = env.WebRootPath;

            _imageService = imageService;
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(string), StatusCodes.Status404NotFound)]
        [ProducesDefaultResponseType]
        public async Task<IActionResult> PostAsync(
            [FromBody]CertificateEntity certificate,
            [FromServices] IUserService userService,
            [FromServices] ICertificateService certificateService) 
        {
            certificate.CertificateId = Guid.NewGuid();

            UserEntity? user = await userService.GetByIdAsync(certificate.UserId);

            if (user is null)
            {
                return NotFound("User not found");
            }

            await certificateService.InsertAsync(certificate);

            return  Ok(certificate);
        }

        [HttpGet("{certificateId}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(string), StatusCodes.Status404NotFound)]
        [ProducesDefaultResponseType]
        public async Task<IActionResult> GetOnceAsync(
            [FromRoute] Guid certificateId,
            [FromServices] ICertificateService certificateService)
        {
            CertificateEntity? certificate = await certificateService.GetByIdAsync(certificateId);

            if (certificate is null)
            {
                return NotFound("Certificate not found");
            }

            return Ok(certificate);
        }

        [HttpGet]
        public async Task<IActionResult> GetCertificatesAsync(
            [FromQuery] Guid userId,
            [FromServices] ICertificateService certificateService)
        {

            IEnumerable<CertificateEntity> certificates = await certificateService.GetAllByUserAsync(userId);

            return Ok(certificates);
        }

        [HttpPut]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(string), StatusCodes.Status401Unauthorized)]
        [ProducesDefaultResponseType]
        public async Task<IActionResult> PutAsync(
            [FromBody] CertificateEntity certificate,
            [FromServices] IUserService userService,
            [FromServices] ICertificateService certificateService)
        {
            UserEntity? user = await userService.GetByIdAsync(certificate.UserId);

            if (user is null)
            {
                return Unauthorized("You are not authorized to update this certificate.");
            }

            CertificateEntity? result = await certificateService.UpdateCertificateAsync(certificate);

            return Ok(result);
        }

        [HttpDelete("{certificateId}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(string), StatusCodes.Status403Forbidden)]
        [ProducesResponseType(typeof(string), StatusCodes.Status404NotFound)]
        [ProducesDefaultResponseType]
        public async Task<IActionResult> DeleteAsync(
            Guid certificateId,
            [FromServices] IUserService userService,
            [FromServices] ICertificateService certificateService,
            [FromServices] IImageService imageService)
        {
            CertificateEntity? certificate = await certificateService.GetByIdAsync(certificateId);

            if (certificate is null)
            {
                return NotFound("Certificate not found");
            }

            UserEntity? user = await userService.GetByIdAsync(certificate.UserId);

            if (user is null)
            {
                return Forbid("You cannot delete this certificate.");
            }

            await imageService.RemoveImageAndDirectoryAsync(
                _subDirectoryCertificates,
                certificate.ImageId,
                user.UserId,
                _wwwrootPath);

            await certificateService.DeleteAsync(certificateId);

            return Ok("Certificate deleted");
        }

        [HttpPost("{certificateId}/image")]
        public async Task<IActionResult> UploadPageImageAsync(
            Guid certificateId,
            [FromForm] IFormFile imageFile,
            [FromServices] ICertificateService certificateService)
        {
            CertificateEntity? certificateEntity = await certificateService.GetByIdAsync(certificateId);

            if (certificateEntity is null)
            {
                return NotFound("Certificate not found");
            }

            ImageEntity imageEntity = await _imageService.UploadImageAsync(
                certificateId,
                imageFile,
                _subDirectoryCertificates,
                _wwwrootPath,
                certificateService.UpdateCertificateWithNewImageAsync);

            if (imageEntity is null)
            {
                return Problem();
            }

            return Ok(imageEntity);
        }
    }
}
