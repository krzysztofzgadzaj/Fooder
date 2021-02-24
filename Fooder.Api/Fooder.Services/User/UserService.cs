using System.Threading.Tasks;
using Fooder.Constants;
using Fooder.Dto.ViewModel;
using Fooder.Helpers.Identity;

namespace Fooder.Services.User
{
    public sealed class UserService : IUserService
    {
        private readonly IIdentityPort _identityPort;

        public UserService(IIdentityPort identityPort)
        {
            _identityPort = identityPort;
        }

        public async Task<UserViewModel> AuthenticateUserAsync(string login, string password) =>
            await _identityPort.AuthenticateAsync(login, password, IdentityConstants.ScopeName);

        public async Task<AuthorizationResultViewModel> AuthorizeAsync(string permissionCode) =>
            await _identityPort.AuthorizeAsync(permissionCode);

        public async Task<bool> CreateUserAsync(
            string login,
            string password,
            string name,
            string lastName,
            string mailAddress) =>
            await _identityPort.AddUserAsync(
                login,
                password,
                name,
                lastName,
                mailAddress);
    }
}
