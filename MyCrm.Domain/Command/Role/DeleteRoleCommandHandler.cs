using System.Threading.Tasks;
using MyCrm.Domain.Repositories;

namespace MyCrm.Domain.Command.Role
{
    public sealed class DeleteRoleCommandHandler : ICommandHandler<DeleteRoleCommand>
    {
        private readonly IUnitOfWork _unitOfWork;

        public DeleteRoleCommandHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<Result> HandleAsync(DeleteRoleCommand command)
        {
            var role = await _unitOfWork.RolesRepository.GetAsync(command.Id);
            if (role == null)
            {
                return Result.Fail("Role does not exist.");
            }

            await _unitOfWork.RolesRepository.DeleteAsync(role);
            await _unitOfWork.CommitAsync();

            return Result.Ok();
        }
    }
}
