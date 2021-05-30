using System;

namespace MyCrm.Domain.Command.Order
{
    public class DeleteOrderCommand : ICommand
    {
        public DeleteOrderCommand(Guid id)
        {
            Id = id;
        }

        public Guid Id { get; }
    }
}
