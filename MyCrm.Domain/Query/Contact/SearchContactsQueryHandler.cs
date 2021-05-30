using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using FluentValidation;
using MyCrm.Domain.Query.Dto;
using MyCrm.Domain.Query.Dto.Pagination.PageResults;
using MyCrm.Domain.Repositories;

namespace MyCrm.Domain.Query.Contact
{
    public sealed class SearchContactsQueryHandler : IQueryHandler<SearchContactsQuery, ContactPageResult<ContactDto>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public SearchContactsQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }

        public async Task<ContactPageResult<ContactDto>> HandleAsync(SearchContactsQuery query)
        {
            var validationResult = await new SearchContactsQueryValidator().ValidateAsync(query);

            if (!validationResult.IsValid)
            {
                throw new ValidationException(validationResult.Errors);
            }

            var result = await _unitOfWork.ContactsRepository.SearchAsync(
                query.UserId,
                query.SearchPhrase,
                query.PageNumber,
                query.PageSize,
                query.OrderBy,
                query.SortDirection
            );

            return new ContactPageResult<ContactDto>(_mapper.Map<List<ContactDto>>(result.Items), result.TotalItemsCount, query.PageSize, query.PageNumber);
        }
    }
}
