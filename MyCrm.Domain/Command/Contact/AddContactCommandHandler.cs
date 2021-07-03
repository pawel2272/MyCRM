using System;
using System.Threading.Tasks;
using AutoMapper;
using MyCrm.Domain.Repositories;

namespace MyCrm.Domain.Command.Contact
{
    public sealed class AddContactCommandHandler : ICommandHandler<AddContactCommand>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public AddContactCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }

        public async Task<Result> HandleAsync(AddContactCommand command)
        {
            var validationResult = await new AddContactCommandValidator().ValidateAsync(command);

            if (!validationResult.IsValid)
            {
                return Result.Fail(validationResult);
            }

            var contact = _mapper.Map<Entities.Contact>(command);
            contact.Id = Guid.NewGuid();

            contact.CreDate = DateTime.Now;
            contact.ModDate = DateTime.Now;

            await _unitOfWork.ContactsRepository.AddAsync(contact);
            await _unitOfWork.CommitAsync();

            return Result.Ok();
        }
    }
}
