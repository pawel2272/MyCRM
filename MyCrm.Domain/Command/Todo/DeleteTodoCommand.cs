using System;

namespace MyCrm.Domain.Command.Todo
{
    public sealed class DeleteTodoCommand : ICommand
    {
        public DeleteTodoCommand(Guid id)
        {
            Id = id;
        }

        public Guid Id { get; }
    }
}
