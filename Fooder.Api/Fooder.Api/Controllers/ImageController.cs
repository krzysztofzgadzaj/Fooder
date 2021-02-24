using System;
using System.Threading;
using System.Threading.Tasks;
using Fooder.Services.Image;
using Microsoft.AspNetCore.Mvc;
using Fooder.Dto.ViewModel;

namespace Fooder.Api.Controllers
{
    [ApiController]
    [Route(RoutePattern)]
    public sealed class ImageController : BaseController
    {
        private readonly IImageService _imageService;

        public ImageController(IImageService imageService)
        {
            _imageService = imageService;
        }

        [HttpPost]
        public async Task<IActionResult> AddImagesAsync([FromBody] Guid ownerId)
        {
            await _imageService.AddImagesAsync(Request.Form.Files, ownerId);
            return Ok();
        }

        [HttpGet(ResourceIdentifierPattern)]
        public async Task<ActionResult<FileViewModel>> GetImageAsync(
            [FromRoute] int id,
            CancellationToken cancellationToken) =>
                Ok(await _imageService.GetByIdAsync(id, cancellationToken));
    }
}
