using System.Threading.Tasks;
using MyCrm.Domain.Repositories;

namespace MyCrm.Domain.Command.User
{
    public sealed class DeleteUserCommandHandler : ICommandHandler<DeleteUserCommand>
    {
        private readonly IUnitOfWork _unitOfWork;

        public DeleteUserCommandHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<Result> HandleAsync(DeleteUserCommand command)
        {
            var user = await _unitOfWork.UsersRepository.GetAsync(command.Id);
            if (user == null)
            {
                return Result.Fail("User does not exist.");
            }

            await _unitOfWork.UsersRepository.DeleteAsync(user);
            await _unitOfWork.CommitAsync();

            return Result.Ok();
        }
    }
}
