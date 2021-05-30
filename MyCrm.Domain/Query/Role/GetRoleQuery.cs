using MyCrm.Domain.Query.Dto;
using System;

namespace MyCrm.Domain.Query.Role
{
    public class GetRoleQuery : IQuery<RoleDto>
    {
        public GetRoleQuery(Guid id)
        {
            Id = id;
        }

        public Guid Id { get; }
    }
}
