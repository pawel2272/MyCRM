using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using FluentValidation;
using MyCrm.Domain.Query.Dto;
using MyCrm.Domain.Query.Dto.Pagination.PageResults;
using MyCrm.Domain.Repositories;

namespace MyCrm.Domain.Query.Order
{
    public sealed class SearchOrdersQueryHandler : IQueryHandler<SearchOrdersQuery, OrderPageResult<OrderDto>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public SearchOrdersQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }

        public async Task<OrderPageResult<OrderDto>> HandleAsync(SearchOrdersQuery query)
        {
            var validationResult = await new SearchOrdersQueryValidator().ValidateAsync(query);

            if (!validationResult.IsValid)
            {
                throw new ValidationException(validationResult.Errors);
            }

            var result = await _unitOfWork.OrdersRepository.SearchAsync(
                query.ContactId,
                query.SearchPhrase,
                query.PageNumber,
                query.PageSize,
                query.OrderBy,
                query.SortDirection
            );

            return new OrderPageResult<OrderDto>(_mapper.Map<List<OrderDto>>(result.Items), result.TotalItemsCount, query.PageSize, query.PageNumber, result.ContactId);
        }
    }
}
