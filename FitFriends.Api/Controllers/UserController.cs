using FitFriends.ServiceLibrary.Domains.Contracts;
using FitFriends.ServiceLibrary.Entities;
using Microsoft.AspNetCore.Mvc;
using OfficeOpenXml;
using FitFriends.ServiceLibrary.Clients.Contracts;
using FitFriends.ServiceLibrary.QueryParameters;
using FitFriends.ServiceLibrary.QueryFilters.MoySklad;
using FitFriends.ServiceLibrary.Domains;

namespace FitFriends.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IWebHostEnvironment _env;

        private readonly IImageService _imageService;

        public UserController(IWebHostEnvironment env, IImageService imageService)
        {
            _env = env;

            _imageService = imageService;
        }

        [HttpPost]
        public async Task<IActionResult> PostAsync(
            [FromBody]UserEntity user,
            [FromServices] IUserService userService) 
        {
            await userService.InsertAsync(user);

            return  Ok(user);
        }

        [HttpGet("{userId}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(string), StatusCodes.Status404NotFound)]
        [ProducesDefaultResponseType]
        public async Task<IActionResult> GetOnceAsync(
            [FromRoute]Guid userId,
            [FromServices] IUserService userService)
        {
            UserEntity user = await userService.GetByIdAsync(userId);

            if (user is null)
            {
                
                return NotFound("User not found");
            }

            return Ok(user);
        }

        [HttpGet]
        public async Task<IActionResult> GetUsersAsync(
            [FromServices] IUserService userService,
            [FromQuery] PaginationParameters? pagination = null)
        {
            if (pagination is not null)
            {

            }
            var users = await userService.GetAllAsync(pagination);

            return Ok(users);
        }

        [HttpPut]
        public async Task<IActionResult> PutAsync(
            [FromBody] UserEntity user, 
            [FromServices] IUserService userService)
        {
            UserEntity? result = await userService.UpdateUserAsync(user);

            return Ok(result);
        }

        [HttpDelete("{userId}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(string), StatusCodes.Status404NotFound)]
        [ProducesDefaultResponseType]
        public async Task<IActionResult> DeleteAsync(
            Guid userId, 
            [FromServices] IUserService userService)
        {
            UserEntity? user = await userService.GetByIdAsync(userId);

            if (user is null)
            {
                return NotFound("User not found");
            }

            string wwwrootPath = _env.WebRootPath;

            await _imageService.RemoveImageAndDirectoryAsync(
                "Avatars", 
                user.AvatarId, 
                user.UserId, 
                wwwrootPath);

            await _imageService.RemoveImageAndDirectoryAsync(
                "PageImages", 
                user.PageImageId, 
                user.UserId, 
                wwwrootPath);

            await userService.DeleteAsync(userId);

            return Ok("User deleted");
        }

        [HttpGet("export-to-excel")]
        public async Task<IActionResult> ExportToExcel(
            [FromServices] IUserService userService,
            [FromQuery] PaginationParameters? pagination = null)
        {
            var users = await userService.GetAllAsync(pagination);

            using var package = new ExcelPackage();
            var worksheet = package.Workbook.Worksheets.Add("Users");

            worksheet.Cells["A1"].Value = "UserId";
            worksheet.Cells["B1"].Value = "Name";
            worksheet.Cells["C1"].Value = "Email";

            var row = 2;
            foreach (var user in users)
            {
                worksheet.Cells[row, 1].Value = user.UserId;
                worksheet.Cells[row, 2].Value = user.Name;
                worksheet.Cells[row, 3].Value = user.Email;

                row++;
            }

            var stream = new MemoryStream();
            package.SaveAs(stream);

            stream.Position = 0;

            return File(stream, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "Users.xlsx");
        }

        [HttpGet("assortment")]
        public async Task<IActionResult> GetAssortment(
            [FromServices] IMoyskladService moyskladService,
            [FromQuery] PaginationParameters? pagination = null,
            [FromQuery] AssortmentFilterParameters? filters = null)
        {
            var assortmentResponse = await moyskladService.GetFilteredAssortmentsAsync(pagination, filters);

            return Ok(assortmentResponse.Payload);
        }

        [HttpPost("{userId}/avatar")]
        public async Task<IActionResult> UploadAvatarAsync(
           Guid userId,
           [FromForm] IFormFile avatarFile,
           [FromServices] IUserService userService)
        {
            UserEntity? user = await userService.GetByIdAsync(userId);

            if (user is null)
            {
                return NotFound("User not found");
            }

            return await UploadImageAsync(
                userId, 
                avatarFile, 
                "Avatars",
                userService.UpdateUserWithNewAvatarAsync);
        }

        [HttpPost("{userId}/pageImage")]
        public async Task<IActionResult> UploadPageImageAsync(
            Guid userId,
            [FromForm] IFormFile pageImageFile,
            [FromServices] IUserService userService)
        {
            UserEntity? user = await userService.GetByIdAsync(userId);

            if (user is null)
            {
                return NotFound("User not found");
            }

            return await UploadImageAsync(
                userId, 
                pageImageFile, 
                "PageImages",
                userService.UpdateUserWithNewPageImageAsync);
        }

        private async Task<IActionResult> UploadImageAsync(
            Guid id,
            IFormFile imageFile,
            string subDirName,
            Func<Guid, ImageEntity, string, Task> updateOperation)
        {
            if (imageFile is null)
            {
                return BadRequest($"The {nameof(ImageEntity)} file field is required.");
            }

            string wwwrootPath = _env.WebRootPath;
            string subDirPath = $"{nameof(ImageEntity)}{id}";

            DirectoryInfo directoryInfo = new(Path.Combine(wwwrootPath, subDirName));

            if (!directoryInfo.Exists)
            {
                directoryInfo.Create();
            }

            directoryInfo.CreateSubdirectory(subDirPath);

            string fileName = Path.GetFileNameWithoutExtension(imageFile.FileName);
            string extension = Path.GetExtension(imageFile.FileName);
            string imageTitle = $"{fileName}{id}{extension}";

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

            await updateOperation(id, imageEntity, wwwrootPath);

            return Ok(imageEntity);
        }
    }
}
