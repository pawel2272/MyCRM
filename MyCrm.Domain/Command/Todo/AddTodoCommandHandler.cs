using System;
using System.Threading.Tasks;
using AutoMapper;
using MyCrm.Domain.Repositories;

namespace MyCrm.Domain.Command.Todo
{
    public sealed class AddTodoCommandHandler : ICommandHandler<AddTodoCommand>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public AddTodoCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }

        public async Task<Result> HandleAsync(AddTodoCommand command)
        {
            var validationResult = await new AddTodoCommandValidator().ValidateAsync(command);

            if (!validationResult.IsValid)
            {
                return Result.Fail(validationResult);
            }

            var todo = _mapper.Map<Entities.Todo>(command);
            todo.CreDate = DateTime.Now;
            todo.ModDate = DateTime.Now;
            await _unitOfWork.TodosRepository.AddAsync(todo);
            await _unitOfWork.CommitAsync();

            return Result.Ok();
        }
    }
}
