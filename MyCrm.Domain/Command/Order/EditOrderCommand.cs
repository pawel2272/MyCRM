using System;

namespace MyCrm.Domain.Command.Order
{
    public class EditOrderCommand : ICommand
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public decimal Price { get; set; }
        public Guid ContactId { get; set; }
    }
}
