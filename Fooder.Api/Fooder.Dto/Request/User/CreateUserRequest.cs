namespace Fooder.Dto.Request.User
{
    public sealed class CreateUserRequest
    {
        public string Login { get; set; }
        public string Password { get; set; }
        public string Name { get; set; }
        public string LastName { get; set; }
        public string MailAddress { get; set; }
    }
}
