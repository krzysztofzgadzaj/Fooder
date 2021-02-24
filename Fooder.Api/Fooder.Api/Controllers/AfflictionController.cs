using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Fooder.Dto.ViewModel;
using Fooder.Services.Affliction;
using Microsoft.AspNetCore.Mvc;

namespace Fooder.Api.Controllers
{
    [ApiController]
    [Route(RoutePattern)]
    public sealed class AfflictionController : BaseController
    {
        private readonly IAfflictionService _afflictionService;

        public AfflictionController(IAfflictionService afflictionService)
        {
            _afflictionService = afflictionService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<AfflictionViewModel>>> GetRangeAsync(CancellationToken cancellationToken)
        {
            var result = await _afflictionService.GetRangeAsync(cancellationToken);
            return Ok(result);
        }
    }
}
