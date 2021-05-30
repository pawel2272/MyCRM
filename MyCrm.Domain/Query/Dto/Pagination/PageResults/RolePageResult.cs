using System;
using System.Collections.Generic;

namespace MyCrm.Domain.Query.Dto.Pagination.PageResults
{
    public class RolePageResult<T>
    {
        public List<T> Items { get; }
        public int TotalPages { get; }
        public int ItemsFrom { get; }
        public int ItemsTo { get; }
        public int TotalItemsCount { get; }

        public RolePageResult(List<T> items, int totalCount, int pageSize, int pageNumber)
        {
            Items = items;
            TotalItemsCount = totalCount;
            ItemsFrom = pageSize * (pageNumber - 1) + 1;
            ItemsTo = ItemsFrom + pageSize - 1;
            TotalPages = (int)Math.Ceiling(totalCount / (double)pageSize);
        }
    }
}
