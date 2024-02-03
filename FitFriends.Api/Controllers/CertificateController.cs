using FitFriends.ServiceLibrary.Domains;
using FitFriends.ServiceLibrary.Domains.Contracts;
using FitFriends.ServiceLibrary.Entities;
using Microsoft.AspNetCore.Mvc;

namespace FitFriends.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CertificateController : ControllerBase
    {
        private readonly IWebHostEnvironment _env;
        public CertificateController(IWebHostEnvironment env)
        {
            _env = env;
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

            UserEntity user = await userService.GetByIdAsync(certificate.UserId);

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

            var certificates = await certificateService.GetAllByUserAsync(userId);

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
            UserEntity user = await userService.GetByIdAsync(certificate.UserId);

            if (user is null)
            {
                return Unauthorized("You are not authorized to update this certificate.");
            }

            CertificateEntity? result = await certificateService.UpdateAsync(certificate);

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

            UserEntity user = await userService.GetByIdAsync(certificate.UserId);

            if (user is null)
            {
                return Forbid("You cannot delete this certificate.");
            }

            if (certificate.ImageId is not null)
            {
                await imageService.RemoveAsync((Guid)certificate.ImageId);
            }

            await certificateService.DeleteAsync(certificateId);

            return Ok("Certificate deleted");
        }

        [HttpPost("{certificateId}/image")]
        public async Task<IActionResult> UploadImageAsync(
            Guid certificateId,
            [FromForm] IFormFile imageFile,
            [FromServices] ICertificateService certificateService)
        {
            CertificateEntity? certificate = await certificateService.GetByIdAsync(certificateId);

            if (certificate is null)
            {
                return NotFound("Certificate not found");
            }

            if (imageFile is null)
            {
                return BadRequest("The image file field is required.");
            }

            string wwwrootPath = _env.WebRootPath;

            string subDirPath = $"Certificate{certificateId}";

            DirectoryInfo directoryInfo = new(Path.Combine(wwwrootPath, "Certificates"));

            if (directoryInfo.Exists)
            {
                directoryInfo.Create();
            }

            directoryInfo.CreateSubdirectory(subDirPath);

            string fileName = Path.GetFileNameWithoutExtension(imageFile.FileName);

            string extension = Path.GetExtension(imageFile.FileName);

            certificate.Image = new()
            {
                ImageFile = imageFile,
                ImageTitle = $"{fileName}{certificateId}{extension}"
            };

            string path = Path.Combine(wwwrootPath, "Certificates", subDirPath, certificate.Image.ImageTitle);

            using (FileStream fileStream = new(path, FileMode.Create))
            {
                await certificate.Image.ImageFile.CopyToAsync(fileStream);
            }

            await certificateService.UpdateAsync(certificate);

            return Ok($"Image uploaded for cetificate id {certificateId}");
        }
    }
}
