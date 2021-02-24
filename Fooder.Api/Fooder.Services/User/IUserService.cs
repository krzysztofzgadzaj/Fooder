using System.Threading.Tasks;
using Fooder.Dto.ViewModel;

namespace Fooder.Services.User
{
    public interface IUserService
    {
        Task<UserViewModel> AuthenticateUserAsync(string login, string password);
        Task<AuthorizationResultViewModel> AuthorizeAsync(string permissionCode);
        Task<bool> CreateUserAsync(
            string login,
            string password,
            string name,
            string lastName,
            string mailAddress);
    }
}
