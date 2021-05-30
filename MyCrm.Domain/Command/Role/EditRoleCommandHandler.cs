using System.Threading.Tasks;
using AutoMapper;
using MyCrm.Domain.Repositories;

namespace MyCrm.Domain.Command.Role
{
    public sealed class EditRoleCommandHandler : ICommandHandler<EditRoleCommand>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public EditRoleCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }

        public async Task<Result> HandleAsync(EditRoleCommand command)
        {
            var validationResult = await new EditRoleCommandValidator().ValidateAsync(command);
            if (!validationResult.IsValid)
            {
                return Result.Fail(validationResult);
            }

            var role = await _unitOfWork.RolesRepository.GetAsync(command.Id);
            if (role == null)
            {
                return Result.Fail("Role does not exist.");
            }

            _mapper.Map(command, role);

            await _unitOfWork.RolesRepository.UpdateAsync(role);
            await _unitOfWork.CommitAsync();

            return Result.Ok();
        }
    }
}
