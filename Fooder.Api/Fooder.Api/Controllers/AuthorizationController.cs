using System.Threading.Tasks;
using Fooder.Dto.ViewModel;
using Fooder.Services.User;
using Microsoft.AspNetCore.Mvc;

namespace Fooder.Api.Controllers
{
    [ApiController]
    [Route(RoutePattern)]
    public sealed class AuthorizationController : BaseController
    {
        private readonly IUserService _userService;

        public AuthorizationController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpGet]
        public async Task<ActionResult<AuthorizationResultViewModel>>
            AuthorizeAsync([FromQuery] string permissionCode) =>
                Ok(await _userService.AuthorizeAsync(permissionCode));
    }
}
