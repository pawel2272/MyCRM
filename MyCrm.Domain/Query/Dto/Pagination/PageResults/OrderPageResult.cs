using System;
using System.Collections.Generic;

namespace MyCrm.Domain.Query.Dto.Pagination.PageResults
{
    public class OrderPageResult<T>
    {
        public Guid ContactId { get; }
        public List<T> Items { get; }
        public int TotalPages { get; }
        public int ItemsFrom { get; }
        public int ItemsTo { get; }
        public int TotalItemsCount { get; }

        public OrderPageResult(List<T> items, int totalCount, int pageSize, int pageNumber, Guid contactId)
        {
            ContactId = contactId;
            Items = items;
            TotalItemsCount = totalCount;
            ItemsFrom = pageSize * (pageNumber - 1) + 1;
            ItemsTo = ItemsFrom + pageSize - 1;
            TotalPages = (int)Math.Ceiling(totalCount / (double)pageSize);
        }
    }
}