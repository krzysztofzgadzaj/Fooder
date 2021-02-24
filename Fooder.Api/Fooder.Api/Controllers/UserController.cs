using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Fooder.Dto.Request.User;
using Fooder.Dto.ViewModel;
using Fooder.Services.Pet;
using Fooder.Services.User;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Fooder.Api.Controllers
{
    [ApiController]
    [Route(RoutePattern)]
    public sealed class UserController : BaseController
    {
        private readonly IUserService _userService;
        private readonly IPetService _petService;

        public UserController(IUserService userService,
            IPetService petService)
        {
            _userService = userService;
            _petService = petService;
        }

        [HttpPost]
        public async Task<IActionResult> CreateUserAsync([FromBody] CreateUserRequest createUserRequest)
        {
            var result = await _userService.CreateUserAsync(
                createUserRequest.Login,
                createUserRequest.Password,
                createUserRequest.Name,
                createUserRequest.LastName,
                createUserRequest.MailAddress);

            return result
                ? Ok()
                : StatusCode(StatusCodes.Status409Conflict);
        }

        [HttpGet("{ownerId:int}/pets")]
        public async Task<ActionResult<ICollection<PetViewModel>>> GetUserPetsAsync(
            [FromRoute] int ownerId,
            CancellationToken cancellationToken) =>
                Ok(await _petService.GetOwnerPetsAsync(ownerId, cancellationToken));
    }
}
