using System;
using MyCrm.Domain.Entities;
using MyCrm.Domain.Enums;
using System.Collections.Generic;
using System.Threading.Tasks;
using MyCrm.Domain.Query.Dto.Pagination.PageResults;

namespace MyCrm.Domain.Repositories
{
    public interface IRolesRepository
    {
        Task<Role> GetAsync(Guid id);
        Task DeleteAsync(Role role);
        Task<RolePageResult<Role>> SearchAsync(string searchPhrase, int pageNumber, int pageSize, string orderBy, SortDirection sortDirection);
        Task AddAsync(Role role);
        Task UpdateAsync(Role role);
    }
}
