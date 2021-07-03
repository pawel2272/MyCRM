using System;

namespace MyCrm.Domain.Command.Todo
{
    public sealed class DeleteTodoCommand : ICommand
    {
        public DeleteTodoCommand()
        {
            
        }

        public DeleteTodoCommand(Guid id, Guid contactId)
        {
            ContactId = contactId;
            Id = id;
        }

        public Guid ContactId { get; set; }
        public Guid Id { get; set; }
    }
}
