using MyCrm.Domain.Entities;
using MyCrm.Domain.Enums;
using MyCrm.Domain.Query.Dto.Pagination.PageResults;
using MyCrm.Domain.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace MyCrm.Infrastructure.Repositories
{
    public sealed class RolesRepository : IRolesRepository
    {
        private readonly CRMContext _dbContext;

        public RolesRepository(CRMContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task AddAsync(Role role)
        {
            await _dbContext.Roles.AddAsync(role);
        }

        public async Task DeleteAsync(Role role)
        {
            _dbContext.Roles.Remove(role);
        }

        public async Task<Role> GetAsync(Guid id)
        {
            var role = await _dbContext.Roles.FirstOrDefaultAsync(r => r.Id == id);
            return role;
        }

        public async Task<RolePageResult<Role>> SearchAsync(string searchPhrase, int pageNumber, int pageSize, string orderBy, SortDirection sortDirection)
        {
            var baseQuery = _dbContext.Roles
                .Where(r => searchPhrase == null ||
                                r.Name.ToLower().Contains(searchPhrase.ToLower()));
            if (!string.IsNullOrEmpty(orderBy))
            {
                var columnSelectors = new Dictionary<string, Expression<Func<Role, object>>>()
                {
                    { nameof(Role.Name), r => r.Name },
                };

                var selectedColumn = columnSelectors[orderBy];

                baseQuery = sortDirection == SortDirection.ASC ? baseQuery.OrderBy(selectedColumn) : baseQuery.OrderByDescending(selectedColumn);
            }
            var orders = await baseQuery.Skip(pageSize * (pageNumber - 1))
                .Take(pageSize)
                .ToListAsync();
            return new RolePageResult<Role>(orders, baseQuery.Count(), pageSize, pageNumber);
        }

        public async Task UpdateAsync(Role role)
        {
            _dbContext.Roles.Update(role);
        }
    }
}
