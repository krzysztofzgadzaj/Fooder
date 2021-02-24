namespace Fooder.Dto.Request.User
{
    public sealed class AuthenticateUserRequest
    {
        public string Login { get; set; }
        public string Password { get; set; }
    }
}
