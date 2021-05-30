using System;
using MyCrm.Domain.Entities;
using MyCrm.Domain.Enums;
using System.Threading.Tasks;
using MyCrm.Domain.Query.Dto.Pagination.PageResults;

namespace MyCrm.Domain.Repositories
{
    public interface IOrdersRepository
    {
        Task<Order> GetAsync(Guid id);
        Task DeleteAsync(Order order);
        Task<OrderPageResult<Order>> SearchAsync(Guid contactId, string searchPhrase, int pageNumber, int pageSize, string orderBy, SortDirection sortDirection);
        Task AddAsync(Order order);
        Task UpdateAsync(Order order);
    }
}
