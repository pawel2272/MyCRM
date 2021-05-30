using MyCrm.Domain.Entities;
using MyCrm.Test.Unit.Models;
using System;

namespace MyCrm.Test.Unit
{
    public class SystemUnderTest : IDisposable
    {
        public void Dispose()
        {
            
        }

        public Contact CreateContact()
        {
            var contact = new ContactProxy("Jan", "Kowalski", "123456789", "jan@kowalski.pl", "Miodowa 12", "00-000", "Warszawa", "Sample comment");

            contact.AddOrder("Test", "Test", 1.1m);
            contact.AddOrder("Test2", "Test2", 3.14m);

            contact.AddTodo("Test", "Test");
            contact.AddTodo("Test2", "Test2");

            return contact;
        }
    }
}
