using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;

namespace Fooder.Api.Utils.AuthorizationPolicies
{
    internal sealed class FeedAdditionPermittedPolicy : BaseAuthorizationPolicy<FeedAdditionPermittedPolicy>
    {
        private const string FeedAdditionPermissionCode = "Fooder_Add_Feed";
        
        internal FeedAdditionPermittedPolicy(IServiceProvider serviceProvider)
            : base(serviceProvider)
        {
        }

        protected override async Task HandleRequirementAsync(
            AuthorizationHandlerContext context,
            FeedAdditionPermittedPolicy requirement)
        {
            var isAuthorized = await CheckIsAuthorizedAsync(FeedAdditionPermissionCode);
            HandleAuthorizationResult(isAuthorized, context);
        }
    }
}
