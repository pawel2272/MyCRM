using System;

namespace MyCrm.Domain.Command.User
{
    public sealed class DeleteUserCommand : ICommand
    {
        public DeleteUserCommand(Guid id)
        {
            Id = id;
        }

        public Guid Id { get; }
    }
}
