using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;

namespace Fooder.Api.Utils.AuthorizationPolicies
{
    internal sealed class BrandAdditionPermittedPolicy : BaseAuthorizationPolicy<BrandAdditionPermittedPolicy>
    {
        private const string BrandAdditionPermissionCode = "Fooder_Add_Brand";

        internal BrandAdditionPermittedPolicy(IServiceProvider serviceProvider)
            : base(serviceProvider)
        {
        }

        protected override async Task HandleRequirementAsync(
            AuthorizationHandlerContext context,
            BrandAdditionPermittedPolicy requirement)
        {
            var isAuthorized = await CheckIsAuthorizedAsync(BrandAdditionPermissionCode);
            HandleAuthorizationResult(isAuthorized, context);
        }
    }
}
