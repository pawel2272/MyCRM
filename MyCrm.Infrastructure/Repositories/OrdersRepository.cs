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
    public sealed class OrdersRepository : IOrdersRepository
    {
        private readonly CRMContext _dbContext;

        public OrdersRepository(CRMContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task AddAsync(Order order)
        {
            await _dbContext.Orders.AddAsync(order);
        }

        public async Task DeleteAsync(Order order)
        {
            _dbContext.Orders.Remove(order);
        }

        public async Task<Order> GetAsync(Guid id)
        {
            var order = await _dbContext.Orders.FirstOrDefaultAsync(o => o.Id == id);
            return order;
        }

        public async Task<OrderPageResult<Order>> SearchAsync(Guid contactId, string searchPhrase, int pageNumber, int pageSize, string orderBy, SortDirection sortDirection)
        {
            var baseQuery = _dbContext.Orders
                .Where(o => o.ContactId == contactId
                            && (searchPhrase == null ||
                                o.Title.ToLower().Contains(searchPhrase.ToLower())
                                || o.Content.ToLower().Contains(searchPhrase.ToLower())));
            if (!string.IsNullOrEmpty(orderBy))
            {
                var columnSelectors = new Dictionary<string, Expression<Func<Order, object>>>()
                {
                    { nameof(Order.Title), o => o.Title },
                    { nameof(Order.Content), o => o.Content }
                };

                var selectedColumn = columnSelectors[orderBy];

                baseQuery = sortDirection == SortDirection.ASC ? baseQuery.OrderBy(selectedColumn) : baseQuery.OrderByDescending(selectedColumn);
            }
            var orders = await baseQuery.Skip(pageSize * (pageNumber - 1))
                .Take(pageSize)
                .ToListAsync();
            return new OrderPageResult<Order>(orders, baseQuery.Count(), pageSize, pageNumber, contactId);
        }

        public async Task UpdateAsync(Order order)
        {
            _dbContext.Orders.Update(order);
        }
    }
}
