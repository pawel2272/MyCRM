using System;

namespace MyCrm.Domain.Command.Role
{
    public sealed class DeleteRoleCommand : ICommand
    {
        public DeleteRoleCommand(Guid id)
        {
            Id = id;
        }

        public Guid Id { get; }
    }
}
