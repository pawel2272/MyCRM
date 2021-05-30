using System.Threading.Tasks;
using MyCrm.Domain.Repositories;

namespace MyCrm.Domain.Command.Todo
{
    public sealed class DeleteTodoCommandHandler : ICommandHandler<DeleteTodoCommand>
    {
        private readonly IUnitOfWork _unitOfWork;

        public DeleteTodoCommandHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<Result> HandleAsync(DeleteTodoCommand command)
        {
            var todo = await _unitOfWork.TodosRepository.GetAsync(command.Id);
            if (todo == null)
            {
                return Result.Fail("Todo does not exist.");
            }

            await _unitOfWork.TodosRepository.DeleteAsync(todo);
            await _unitOfWork.CommitAsync();

            return Result.Ok();
        }
    }
}
