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

            string wwwrootPath = _env.WebRootPath;

            await imageService.RemoveImageAndDirectoryAsync(
                "Certificates",
                certificate.ImageId,
                user.UserId,
                wwwrootPath);

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

            return await UploadImageAsync(
                certificateId,
                imageFile,
                "Certificates",
                certificateService.UpdateCertificateWithNewImageAsync);
        }

        private async Task<IActionResult> UploadImageAsync(
            Guid Id,
            IFormFile imageFile,
            string subDirName,
            Func<Guid, ImageEntity, string, Task> updateOperation)
        {
            if (imageFile is null)
            {
                return BadRequest($"The {nameof(ImageEntity)} file field is required.");
            }

            string wwwrootPath = _env.WebRootPath;
            string subDirPath = $"{nameof(ImageEntity)}{Id}";

            DirectoryInfo directoryInfo = new(Path.Combine(wwwrootPath, subDirName));

            if (!directoryInfo.Exists)
            {
                directoryInfo.Create();
            }

            directoryInfo.CreateSubdirectory(subDirPath);

            string fileName = Path.GetFileNameWithoutExtension(imageFile.FileName);
            string extension = Path.GetExtension(imageFile.FileName);
            string imageTitle = $"{fileName}{Id}{extension}";

            string path = Path.Combine(wwwrootPath, subDirName, subDirPath, imageTitle);

            using (FileStream fileStream = new(path, FileMode.Create))
            {
                await imageFile.CopyToAsync(fileStream);
            }

            ImageEntity imageEntity = new()
            {
                ImageFile = imageFile,
                ImageTitle = imageTitle
            };

            await updateOperation(Id, imageEntity, wwwrootPath);

            return Ok(imageEntity);
        }
    }
}
