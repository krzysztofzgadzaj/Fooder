namespace Fooder.Dto.ViewModel
{
    public sealed class UserViewModel : BaseViewModel
    {
        public string Login { get; set; }
        public string Name { get; set; }
        public string LastName { get; set; }
        public string MailAddress { get; set; }
        public string Token { get; set; }
    }
}
