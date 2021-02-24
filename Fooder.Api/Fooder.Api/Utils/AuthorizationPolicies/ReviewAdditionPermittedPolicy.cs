using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;

namespace Fooder.Api.Utils.AuthorizationPolicies
{
    internal sealed class ReviewAdditionPermittedPolicy : BaseAuthorizationPolicy<ReviewAdditionPermittedPolicy>
    {
        private const string ReviewAdditionPermissionCode = "Fooder_Add_Review";

        internal ReviewAdditionPermittedPolicy(IServiceProvider serviceProvider)
            : base(serviceProvider)
        {
        }

        protected override async Task HandleRequirementAsync(
            AuthorizationHandlerContext context,
            ReviewAdditionPermittedPolicy requirement)
        {
            var isAuthorized = await CheckIsAuthorizedAsync(ReviewAdditionPermissionCode);
            HandleAuthorizationResult(isAuthorized, context);
        }
    }
}
