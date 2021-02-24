using System.Threading.Tasks;
using Fooder.Dto.Request.User;
using Fooder.Services.User;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Fooder.Api.Controllers
{
    [ApiController]
    [Route(RoutePattern)]
    public sealed class AuthenticationController : BaseController
    {
        private readonly IUserService _userService;

        public AuthenticationController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpPost]
        public async Task<IActionResult> AuthenticateUserAsync(
            [FromBody] AuthenticateUserRequest authenticateUserRequest)
        {
            var result = await _userService.AuthenticateUserAsync(
                authenticateUserRequest.Login,
                authenticateUserRequest.Password);

            if (result is null)
            {
                return StatusCode(StatusCodes.Status401Unauthorized);
            }

            return Ok(result);
        }
    }
}
