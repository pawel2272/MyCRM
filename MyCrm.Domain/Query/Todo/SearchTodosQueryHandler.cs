using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using FluentValidation;
using MyCrm.Domain.Query.Dto;
using MyCrm.Domain.Query.Dto.Pagination.PageResults;
using MyCrm.Domain.Repositories;

namespace MyCrm.Domain.Query.Todo
{
    public class SearchTodosQueryHandler : IQueryHandler<SearchTodosQuery, TodoPageResult<TodoDto>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public SearchTodosQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }

        public async Task<TodoPageResult<TodoDto>> HandleAsync(SearchTodosQuery query)
        {
            var validationResult = await new SearchTodosQueryValidator().ValidateAsync(query);

            if (!validationResult.IsValid)
            {
                throw new ValidationException(validationResult.Errors);
            }

            var result = await _unitOfWork.TodosRepository.SearchAsync(
                query.ContactId,
                query.SearchPhrase,
                query.PageNumber,
                query.PageSize,
                query.OrderBy,
                query.SortDirection
            );

            return new TodoPageResult<TodoDto>(_mapper.Map<List<TodoDto>>(result.Items), result.TotalItemsCount, query.PageSize, query.PageNumber, result.ContactId);
        }
    }
}
