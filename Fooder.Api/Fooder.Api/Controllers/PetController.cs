using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Fooder.Dto.Request.Pet;
using Fooder.Dto.ViewModel;
using Fooder.Persistence.Entities;
using Fooder.Services.Pet;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Fooder.Api.Controllers
{
    [ApiController]
    [Route(RoutePattern)]
    [Authorize]
    public sealed class PetController : BaseController
    {
        private readonly IPetService _petService;

        public PetController(IPetService petService)
        {
            _petService = petService;
        }

        [HttpPost]
        public async Task<ActionResult<PetViewModel>> CreatePetAsync([FromQuery] AddPetRequest request)
        {
            request.Photos = Request.Form.Files;
            var petViewModel = await _petService.AddAsync(request);
            var uri = $"{RoutePattern}/{petViewModel.Id}";

            return Created(uri, petViewModel);
        }

        [HttpGet(ResourceIdentifierPattern)]
        public async Task<ActionResult<PetViewModel>> GetPetAsync([FromRoute] int id,
            CancellationToken cancellationToken) =>
                Ok(await _petService.GetByIdAsync(id, cancellationToken));

        [HttpGet]
        public async Task<ActionResult<ICollection<PetViewModel>>> GetPetsAsync(CancellationToken cancellationToken) =>
            Ok(await _petService.GetAsync(cancellationToken));

        [AllowAnonymous]
        [HttpGet("activity-levels")]
        public async Task<IActionResult> GetDogActivityLevelsAsync(CancellationToken cancellationToken) =>
            Ok(await Task.Run(
                () => Enum.GetValues(typeof(DogActivityLevel))
                    .Cast<DogActivityLevel>()
                    .Select(
                        type => new
                        {
                            Name = type.ToString(),
                            Key = type,
                        })
                    .ToList(),
                cancellationToken));

        [HttpPut(ResourceIdentifierPattern)]
        public async Task<ActionResult<PetViewModel>> UpdatePetAsync([FromBody] UpdatePetRequest request,
            [FromRoute] int id) =>
                Ok(await _petService.UpdateAsync(request, id));

        [HttpDelete(ResourceIdentifierPattern)]
        public async Task<IActionResult> DeletePetAsync([FromRoute] int id)
        {
            await _petService.DeleteAsync(id);
            return Ok();
        }
    }
}
