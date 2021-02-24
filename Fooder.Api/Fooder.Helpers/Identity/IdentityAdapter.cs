using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using Fooder.Constants;
using Fooder.Dto.ViewModel;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;

namespace Fooder.Helpers.Identity
{
    public sealed class IdentityAdapter : IIdentityPort
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IdentityConfiguration _configuration;

        public IdentityAdapter(IOptions<IdentityConfiguration> configuration,
            IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
            _configuration = configuration.Value;
        }

        public async Task<UserViewModel> AuthenticateAsync(string principalLogin, string principalPassword, string scopeName)
        {
            var httpClient = new HttpClient(_configuration.IdentityServiceBaseUrl);
            var jsonBody = JsonConvert.SerializeObject(
                new
                {
                    Login = principalLogin,
                    Password = principalPassword,
                    ScopeName = scopeName,
                });

            var response = await httpClient.PostAsync(IdentityConstants.AuthenticationRoute, jsonBody);

            var isAuthenticated = response.StatusCode == HttpStatusCode.OK;
            var userViewModel = isAuthenticated
                ? JsonConvert.DeserializeObject<UserViewModel>(response.Content)
                : null;

            return userViewModel;
        }

        public async Task<AuthorizationResultViewModel> AuthorizeAsync(string permissionCode)
        {
            var httpClient = new HttpClient(_configuration.IdentityServiceBaseUrl);
            var route = string.Format(IdentityConstants.AuthorizationRoute, permissionCode);
            var token = _httpContextAccessor.HttpContext.Request.Headers[IdentityConstants.AuthorizationHeaderKey];
            var headers = new List<KeyValuePair<string, string>>
            {
                new KeyValuePair<string, string>(
                    IdentityConstants.AuthorizationHeaderKey,
                    token),
            };

            var response = await httpClient.GetAsync(route, headers);
            var hasBeenProcessedCorrectly = response.StatusCode == HttpStatusCode.OK;
            var authorizationResultViewModel = hasBeenProcessedCorrectly
                ? JsonConvert.DeserializeObject<AuthorizationResultViewModel>(response.Content)
                : new AuthorizationResultViewModel();

            return authorizationResultViewModel;
        }

        public async Task<bool> AddUserAsync(
            string principalLogin,
            string password,
            string name,
            string lastName,
            string mailAddress)
        {
            var httpClient = new HttpClient(_configuration.IdentityServiceBaseUrl);
            var jsonBody = JsonConvert.SerializeObject(
                new
                {
                    Login = principalLogin,
                    Password = password,
                    Name = name,
                    LastName = lastName,
                    MailAddress = mailAddress,
                });

            var response = await httpClient.PostAsync(IdentityConstants.CreatePrincipalRoute, jsonBody);

            var hasBeenCreated = response.StatusCode == HttpStatusCode.OK || response.StatusCode == HttpStatusCode.Created;

            return hasBeenCreated;
        }

        public async Task<UserViewModel> GetUserAsync(int userId)
        {
            var httpClient = new HttpClient(_configuration.IdentityServiceBaseUrl);
            var route = string.Format(IdentityConstants.GetUserRoutePattern, userId);
            var token = _httpContextAccessor.HttpContext.Request.Headers[IdentityConstants.AuthorizationHeaderKey];
            var headers = new List<KeyValuePair<string, string>>
            {
                new KeyValuePair<string, string>(
                    IdentityConstants.AuthorizationHeaderKey,
                    token),
            };
            
            var response = await httpClient.GetAsync(route, headers);
            var hasBeenProcessedCorrectly = response.StatusCode == HttpStatusCode.OK;
            var userViewModel = hasBeenProcessedCorrectly
                ? JsonConvert.DeserializeObject<UserViewModel>(response.Content)
                : new UserViewModel();

            if (string.IsNullOrEmpty(userViewModel.Login)) // ToDo: XDDDD
            {
                userViewModel.Login = "Zaloguj się, aby poznać użytkownika";
            }

            return userViewModel;
        }
    }
}
