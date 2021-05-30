namespace MyCrm.Domain.Command.Role
{
    public sealed class AddRoleCommand : ICommand
    {
        public string Name { get; set; }
    }
}