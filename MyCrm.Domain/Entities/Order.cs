using System;

namespace MyCrm.Domain.Entities
{
    public partial class Order
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public decimal Price { get; set; }
        public Guid ContactId { get; set; }
        public DateTime CreDate { get; set; }
        public DateTime ModDate { get; set; }

        public virtual Contact Contact { get; set; }
    }
}
