using System;
using MyCrm.Domain.Entities;
using System.Collections.Generic;

namespace MyCrm.Test.Unit.Models
{
    public class ContactProxy : Contact
    {
        public ContactProxy(string firstName,
            string lastName,
            string phone,
            string email,
            string street,
            string postalCode,
            string city,
            string contactComment) : base()
        {
            this.Id = Guid.NewGuid();
            this.FirstName = firstName;
            this.LastName = lastName;
            this.Phone = phone;
            this.Email = email;
            this.Street = street;
            this.PostalCode = postalCode;
            this.City = city;
            this.ContactComment = contactComment;
            this.Orders = new List<Order>();
            this.Todos = new List<Todo>();
        }

        public void AddOrder(string title,
            string content,
            decimal price)
        {
            this.Orders.Add(new Order
            {
                Title = title,
                Content = content,
                Price = price,
                Contact = this
            });
        }

        public void AddTodo(string title, string content)
        {
            this.Todos.Add(new Todo()
            {
                Title = title,
                Content = content,
                Contact = this
            });
        }
    }
}
