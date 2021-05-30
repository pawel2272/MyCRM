using System;

namespace MyCrm.Domain.Command.Contact
{
    public sealed class AddContactCommand : ICommand
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public string Street { get; set; }
        public string PostalCode { get; set; }
        public string City { get; set; }
        public string ContactComment { get; set; }
        public Guid UserId { get; set; }
    }
}