using System;

namespace MyCrm.Domain.Command.Role
{
    public sealed class EditRoleCommand : ICommand
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
    }
}
