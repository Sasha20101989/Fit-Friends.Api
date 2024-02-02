using FitFriends.ServiceLibrary.Domains.Contracts;
using FitFriends.ServiceLibrary.Entities;
using Microsoft.AspNetCore.Mvc;

namespace FitFriends.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CertificateController : ControllerBase
    {
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
            CertificateEntity certificate = await certificateService.GetByIdAsync(certificateId);

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

            CertificateEntity result = await certificateService.UpdateAsync(certificate);

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
            [FromServices] ICertificateService certificateService)
        {
            CertificateEntity certificate = await certificateService.GetByIdAsync(certificateId);

            if (certificate is null)
            {
                return NotFound("Certificate not found");
            }

            UserEntity user = await userService.GetByIdAsync(certificate.UserId);

            if (user is null)
            {
                return Forbid("You cannot delete this certificate.");
            }        

            await certificateService.DeleteAsync(certificateId);

            return Ok("Certificate deleted");
        }
    }
}
