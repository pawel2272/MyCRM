using System;

namespace MyCrm.Domain.Command.Todo
{
    public sealed class AddTodoCommand : ICommand
    {
        public string Title { get; set; }
        public string Content { get; set; }
        public Guid ContactId { get; set; }
    }
}