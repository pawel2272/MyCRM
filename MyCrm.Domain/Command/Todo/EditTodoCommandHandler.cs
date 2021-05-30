using System;
using System.Threading.Tasks;
using AutoMapper;
using MyCrm.Domain.Repositories;

namespace MyCrm.Domain.Command.Todo
{
    public sealed class EditTodoCommandHandler : ICommandHandler<EditTodoCommand>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public EditTodoCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }

        public async Task<Result> HandleAsync(EditTodoCommand command)
        {
            var validationResult = await new EditTodoCommandValidator().ValidateAsync(command);
            if (!validationResult.IsValid)
            {
                return Result.Fail(validationResult);
            }

            var todo = await _unitOfWork.TodosRepository.GetAsync(command.Id);
            if (todo == null)
            {
                return Result.Fail("Todo does not exist.");
            }

            _mapper.Map(command, todo);

            todo.ModDate = DateTime.Now;

            await _unitOfWork.TodosRepository.UpdateAsync(todo);
            await _unitOfWork.CommitAsync();

            return Result.Ok();
        }
    }
}
