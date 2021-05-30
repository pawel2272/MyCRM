using System;

namespace MyCrm.Domain.Entities
{
    public partial class Todo
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public Guid ContactId { get; set; }
        public DateTime CreDate { get; set; }
        public DateTime ModDate { get; set; }

        public virtual Contact Contact { get; set; }
    }
}
