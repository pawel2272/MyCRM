using System;

namespace MyCrm.Domain.Command.Contact
{
    public class DeleteContactCommand : ICommand
    {
        public DeleteContactCommand(Guid id)
        {
            Id = id;
        }

        public Guid Id { get; }
    }
}
