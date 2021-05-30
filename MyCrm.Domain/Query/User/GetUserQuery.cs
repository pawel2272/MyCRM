using MyCrm.Domain.Query.Dto;
using System;

namespace MyCrm.Domain.Query.User
{
    public sealed class GetUserQuery : IQuery<UserDto>
    {
        public GetUserQuery(Guid id)
        {
            Id = id;
        }

        public Guid Id { get; }
    }
}
