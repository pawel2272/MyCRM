using System;
using MyCrm.Domain.Enums;
using MyCrm.Domain.Query.Dto;
using MyCrm.Domain.Query.Dto.Pagination.PageResults;

namespace MyCrm.Domain.Query.Order
{
    public sealed class SearchOrdersQuery : IQuery<OrderPageResult<OrderDto>>
    {
        public Guid ContactId { get; set; }
        public string SearchPhrase { get; set; }
        public int PageNumber { get; set; }
        public int PageSize { get; set; }
        public string OrderBy { get; set; }
        public SortDirection SortDirection { get; set; }
    }
}
