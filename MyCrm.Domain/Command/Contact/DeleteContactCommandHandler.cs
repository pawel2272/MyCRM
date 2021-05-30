using System.Threading.Tasks;
using MyCrm.Domain.Repositories;

namespace MyCrm.Domain.Command.Contact
{
    public sealed class DeleteContactCommandHandler : ICommandHandler<DeleteContactCommand>
    {
        private readonly IUnitOfWork _unitOfWork;

        public DeleteContactCommandHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<Result> HandleAsync(DeleteContactCommand command)
        {
            var contact = await _unitOfWork.ContactsRepository.GetAsync(command.Id);
            if (contact == null)
            {
                return Result.Fail("Contact does not exist.");
            }

            await _unitOfWork.ContactsRepository.DeleteAsync(contact);
            await _unitOfWork.CommitAsync();

            return Result.Ok();
        }
    }
}
