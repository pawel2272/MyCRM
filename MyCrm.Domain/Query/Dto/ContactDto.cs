using System;

namespace MyCrm.Domain.Query.Dto
{
    public class ContactDto
    {
        public Guid Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public string Street { get; set; }
        public string PostalCode { get; set; }
        public string City { get; set; }
        public string ContactComment { get; set; }
        public DateTime CreDate { get; set; }
        public DateTime ModDate { get; set; }
        public Guid UserId { get; set; }
    }
}