using System;
using System.Threading.Tasks;
using AutoMapper;
using MyCrm.Domain.Query.Dto;
using MyCrm.Domain.Repositories;

namespace MyCrm.Domain.Query.Contact
{
    public sealed class GetContactQueryHandler : IQueryHandler<GetContactQuery, ContactDto>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public GetContactQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }

        public async Task<ContactDto> HandleAsync(GetContactQuery query)
        {
            var contact = await _unitOfWork.ContactsRepository.GetAsync(query.Id);

            if (contact == null)
            {
                throw new NullReferenceException("Contact does not exist!");
            }

            return _mapper.Map<ContactDto>(contact);
        }
    }
}
