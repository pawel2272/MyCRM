using System;
using MyCrm.Domain.Enums;
using MyCrm.Domain.Query.Dto;
using MyCrm.Domain.Query.Dto.Pagination.PageResults;

namespace MyCrm.Domain.Query.Contact
{
    public class SearchContactsQuery : IQuery<ContactPageResult<ContactDto>>
    {
        public Guid UserId { get; set; }
        public string SearchPhrase { get; set; }
        public int PageNumber { get; set; }
        public int PageSize { get; set; }
        public string OrderBy { get; set; }
        public SortDirection SortDirection { get; set; }
    }
}
