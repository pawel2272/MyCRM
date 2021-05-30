using MyCrm.Domain.Entities;
using MyCrm.Domain.Enums;
using MyCrm.Domain.Query.Dto.Pagination.PageResults;
using MyCrm.Domain.Repositories;
using System;
using System.Threading.Tasks;

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
            throw new NotImplementedException();
        }

        public async Task DeleteAsync(Role role)
        {
            throw new NotImplementedException();
        }

        public async Task<Role> GetAsync(Guid id)
        {
            throw new NotImplementedException();
        }

        public async Task<RolePageResult<Role>> SearchAsync(string searchPhrase, int pageNumber, int pageSize, string orderBy, SortDirection sortDirection)
        {
            throw new NotImplementedException();
        }

        public async Task UpdateAsync(Role role)
        {
            throw new NotImplementedException();
        }
    }
}
