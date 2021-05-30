using System;
using MyCrm.Domain.Entities;
using System.Threading.Tasks;
using MyCrm.Domain.Enums;
using MyCrm.Domain.Query.Dto.Pagination.PageResults;

namespace MyCrm.Domain.Repositories
{
    public interface ITodosRepository
    {
        Task<Todo> GetAsync(Guid id);
        Task DeleteAsync(Todo todo);
        Task<TodoPageResult<Todo>> SearchAsync(Guid contactId, string searchPhrase, int pageNumber, int pageSize, string orderBy, SortDirection sortDirection);
        Task AddAsync(Todo todo);
        Task UpdateAsync(Todo todo);
    }
}
