using System;
using MyCrm.Domain.Entities;

namespace MyCrm.Test.Unit.Models
{
    public class OrderProxy : Order
    {
        public OrderProxy(string title, string content, decimal price)
        {
            this.Id = Guid.NewGuid();
            this.Title = title;
            this.Content = content;
            this.Price = price;
            this.CreDate = DateTime.Now;
            this.ModDate = DateTime.Now;
        }

        public void AddContact(Contact contact)
        {
            this.ContactId = contact.Id;
            this.Contact = contact;
        }
    }
}
