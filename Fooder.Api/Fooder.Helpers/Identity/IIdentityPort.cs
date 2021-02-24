using System.Threading.Tasks;
using Fooder.Dto.ViewModel;

namespace Fooder.Helpers.Identity
{
    public interface IIdentityPort
    {
        Task<UserViewModel> AuthenticateAsync(string principalLogin, string principalPassword, string scopeName);
        Task<AuthorizationResultViewModel> AuthorizeAsync(string permissionCode);

        Task<bool> AddUserAsync(
            string principalLogin,
            string password,
            string name,
            string lastName,
            string mailAddress);

        Task<UserViewModel> GetUserAsync(int userId);
    }
}
