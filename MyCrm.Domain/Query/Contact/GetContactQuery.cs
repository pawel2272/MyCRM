using MyCrm.Domain.Query.Dto;
using System;

namespace MyCrm.Domain.Query.Contact
{
    public sealed class GetContactQuery : IQuery<ContactDto>
    {
        public GetContactQuery(Guid id)
        {
            Id = id;
        }

        public Guid Id { get; }
    }
}
