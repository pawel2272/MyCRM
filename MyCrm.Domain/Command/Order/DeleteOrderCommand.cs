using System;

namespace MyCrm.Domain.Command.Order
{
    public class DeleteOrderCommand : ICommand
    {
        public DeleteOrderCommand()
        {
            
        }

        public DeleteOrderCommand(Guid id, Guid contactId)
        {
            ContactId = contactId;
            Id = id;
        }

        public Guid ContactId { get; set; }
        public Guid Id { get; set; }
    }
}
