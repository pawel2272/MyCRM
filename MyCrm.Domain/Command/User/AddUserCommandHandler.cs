using System;
using System.Threading.Tasks;
using AutoMapper;
using MyCrm.Domain.Repositories;

namespace MyCrm.Domain.Command.User
{
    public sealed class AddUserCommandHandler : ICommandHandler<AddUserCommand>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public AddUserCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }

        public async Task<Result> HandleAsync(AddUserCommand command)
        {
            var validationResult = await new AddUserCommandValidator().ValidateAsync(command);

            if (!validationResult.IsValid)
            {
                return Result.Fail(validationResult);
            }

            var user = _mapper.Map<Entities.User>(command);
            user.CreDate = DateTime.Now;
            user.ModDate = DateTime.Now;
            await _unitOfWork.UsersRepository.AddAsync(user);
            await _unitOfWork.CommitAsync();

            return Result.Ok();
        }
    }
}
