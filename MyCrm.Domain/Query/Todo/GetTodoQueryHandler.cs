using System;
using System.Threading.Tasks;
using AutoMapper;
using MyCrm.Domain.Query.Dto;
using MyCrm.Domain.Repositories;

namespace MyCrm.Domain.Query.Todo
{
    public sealed class GetTodoQueryHandler : IQueryHandler<GetTodoQuery, TodoDto>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public GetTodoQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }

        public async Task<TodoDto> HandleAsync(GetTodoQuery query)
        {
            var todo = await _unitOfWork.TodosRepository.GetAsync(query.Id);

            if (todo == null)
            {
                throw new NullReferenceException("Todo does not exist!");
            }

            return _mapper.Map<TodoDto>(todo);
        }
    }
}
