namespace MyCrm.Domain.Command.User
{
    public sealed class LoginCommand : ICommand
    {
        public string Username { get; set; }
        public string Password { get; set; }
        public bool RememberMe { get; set; }
    }
}