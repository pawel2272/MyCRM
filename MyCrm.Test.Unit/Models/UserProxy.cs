using System;
using MyCrm.Domain.Entities;

namespace MyCrm.Test.Unit.Models
{
    public class UserProxy : User
    {
        public UserProxy(string username,
            string gender,
            string password,
            string firstName,
            string lastName,
            string phone,
            string email,
            string street,
            string postalCode,
            string city) : base()
        {
            this.Id = Guid.NewGuid();
            this.Username = username;
            this.Gender = gender;
            this.Password = password;
            this.FirstName = firstName;
            this.LastName = lastName;
            this.Phone = phone;
            this.Email = email;
            this.Street = street;
            this.PostalCode = postalCode;
            this.City = city;
            this.ModDate = DateTime.Now;
            this.CreDate = DateTime.Now;
        }

        public void AddRole(string name)
        {
            this.RoleId = Guid.NewGuid();
            this.Role = new Role()
            {
                Id = this.RoleId,
                Name = name
            };
            this.Role.Users.Add(this);
        }

        public void AddContact(Contact contact)
        {
            this.Contacts.Add(contact);
        }
    }
}
