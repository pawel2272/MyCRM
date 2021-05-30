using System.Threading.Tasks;
using MyCrm.Domain.Repositories;

namespace MyCrm.Domain.Command.User
{
    public sealed class LogoutCommandHandler : ICommandHandler<LogoutCommand>
    {
        private readonly IUnitOfWork _unitOfWork;

        public LogoutCommandHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<Result> HandleAsync(LogoutCommand command)
        {
            await _unitOfWork.UsersRepository.LogoutAsync();
            await _unitOfWork.TokenRepository.DeactivateCurrentAsync();
            return Result.Ok();
        }
    }
}
