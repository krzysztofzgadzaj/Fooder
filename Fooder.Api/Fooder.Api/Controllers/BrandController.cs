using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Fooder.Api.Utils.AuthorizationPolicies;
using Fooder.Dto.Request.Brand;
using Fooder.Dto.ViewModel;
using Fooder.Services.Brand;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Fooder.Api.Controllers
{
    [ApiController]
    [Route(RoutePattern)]
    public sealed class BrandController : BaseController
    {
        private readonly IBrandService _brandService;

        public BrandController(IBrandService brandService)
        {
            _brandService = brandService;
        }
        
        [HttpPost]
        [Authorize(Policy = nameof(BrandAdditionPermittedPolicy))]
        public async Task<ActionResult<BrandViewModel>> CreateBrandAsync([FromBody] AddBrandRequest request)
        {
            var brandViewModel = await _brandService.AddAsync(request);
            var uri = $"{RoutePattern}/{brandViewModel.Id}";

            return Created(uri, brandViewModel);
        }
        
        [HttpGet]
        public async Task<ActionResult<ICollection<BrandViewModel>>>
            GetBrandsAsync(CancellationToken cancellationToken) =>
                Ok(await _brandService.GetAsync(cancellationToken));
    }
}
