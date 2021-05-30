using System;

namespace MyCrm.Domain.Command.Todo
{
    public sealed class EditTodoCommand : ICommand
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public Guid ContactId { get; set; }
    }
}
