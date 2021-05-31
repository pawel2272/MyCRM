using System;
using System.Threading.Tasks;
using AutoMapper;
using MyCrm.Domain.Query.Dto;
using MyCrm.Domain.Repositories;

namespace MyCrm.Domain.Query.User
{
    public sealed class GetUserQueryHandler : IQueryHandler<GetUserQuery, UserDto>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public GetUserQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }

        public async Task<UserDto> HandleAsync(GetUserQuery query)
        {
            var user = await _unitOfWork.UsersRepository.GetAsync(query.Id);

            if (user == null)
            {
                throw new NullReferenceException("User does not exist!");
            }

            return _mapper.Map<UserDto>(user);
        }
    }
}
