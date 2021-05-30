using System;
using MyCrm.Domain.Entities;
using MyCrm.Domain.Enums;
using System.Threading.Tasks;
using MyCrm.Domain.Query.Dto.Pagination.PageResults;

namespace MyCrm.Domain.Repositories
{
    public interface IContactsRepository
    {
        Task<Contact> GetAsync(Guid id);
        Task DeleteAsync(Contact contact);
        Task<ContactPageResult<Contact>> SearchAsync(Guid userId, string searchPhrase, int pageNumber, int pageSize, string orderBy, SortDirection sortDirection);
        Task AddAsync(Contact contact);
        Task UpdateAsync(Contact contact);
    }
}
