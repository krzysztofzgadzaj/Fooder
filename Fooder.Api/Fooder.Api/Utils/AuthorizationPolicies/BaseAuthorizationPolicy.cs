using System;
using Microsoft.AspNetCore.Authorization;
using System.Threading.Tasks;
using Fooder.Services.User;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;

namespace Fooder.Api.Utils.AuthorizationPolicies
{
    internal abstract class BaseAuthorizationPolicy<TAuthorizationPolicy>
        : AuthorizationHandler<TAuthorizationPolicy>,
          IAuthorizationRequirement
          where TAuthorizationPolicy : IAuthorizationRequirement
    {
        private const string BearerTokenHeaderKey = "Authorization";
        
        protected BaseAuthorizationPolicy(IServiceProvider serviceProvider)
        {
            ServiceProvider = serviceProvider;
        }
        
        protected IServiceProvider ServiceProvider { get; }

        protected async Task<bool> CheckIsAuthorizedAsync(string permissionCode)
        {
            var isAuthorized = false;
            var scope = ((ServiceProvider) ServiceProvider).CreateScope();
            var httpContextAccessor = scope.ServiceProvider.GetService<IHttpContextAccessor>();
            var token = httpContextAccessor.HttpContext.Request.Headers[BearerTokenHeaderKey];
            var isTokenProvided = !string.IsNullOrEmpty(token);

            if (isTokenProvided)
            {
                var userService = scope.ServiceProvider.GetService<IUserService>();
                var authorizationResult = await userService.AuthorizeAsync(permissionCode);
                isAuthorized = authorizationResult.IsAuthorized;
            }

            return isAuthorized;
        }

        protected void HandleAuthorizationResult(
            bool authorizationResult,
            AuthorizationHandlerContext context)
        {
            if (authorizationResult)
            {
                context.Succeed(this);
            }
            else
            {
                context.Fail();
            }
        }
    }
}
