using System;

namespace MyCrm.Domain.Command.User
{
    public sealed class AddUserCommand : ICommand
    {
        public string Username { get; set; }
        public string Gender { get; set; }
        public string Password { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public string Street { get; set; }
        public string PostalCode { get; set; }
        public string City { get; set; }
        public Guid RoleId { get; set; }
    }
}