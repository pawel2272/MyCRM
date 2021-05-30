using System.Threading.Tasks;
using AutoMapper;
using MyCrm.Domain.Repositories;

namespace MyCrm.Domain.Command.Role
{
    public sealed class AddRoleCommandHandler : ICommandHandler<AddRoleCommand>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public AddRoleCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }

        public async Task<Result> HandleAsync(AddRoleCommand command)
        {
            var validationResult = await new AddRoleCommandValidator().ValidateAsync(command);

            if (!validationResult.IsValid)
            {
                return Result.Fail(validationResult);
            }

            var role = _mapper.Map<Entities.Role>(command);
            await _unitOfWork.RolesRepository.AddAsync(role);
            await _unitOfWork.CommitAsync();

            return Result.Ok();
        }
    }
}
