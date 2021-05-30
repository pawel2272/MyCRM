using System;
using System.Threading.Tasks;
using AutoMapper;
using MyCrm.Domain.Repositories;

namespace MyCrm.Domain.Command.Contact
{
    public sealed class EditContactCommandHandler : ICommandHandler<EditContactCommand>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public EditContactCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }

        public async Task<Result> HandleAsync(EditContactCommand command)
        {
            var validationResult = await new EditContactCommandValidator().ValidateAsync(command);
            if (!validationResult.IsValid)
            {
                return Result.Fail(validationResult);
            }

            var contact = await _unitOfWork.ContactsRepository.GetAsync(command.Id);
            if (contact == null)
            {
                return Result.Fail("Contact does not exist.");
            }

            _mapper.Map(command, contact);

            contact.ModDate = DateTime.Now;

            await _unitOfWork.ContactsRepository.UpdateAsync(contact);
            await _unitOfWork.CommitAsync(); 

            return Result.Ok();
        }
    }
}
