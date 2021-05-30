using System;
using System.Threading.Tasks;
using AutoMapper;
using MyCrm.Domain.Query.Dto;
using MyCrm.Domain.Repositories;

namespace MyCrm.Domain.Query.Role
{
    public sealed class GetRoleQueryHandler : IQueryHandler<GetRoleQuery, RoleDto>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public GetRoleQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }

        public async Task<RoleDto> HandleAsync(GetRoleQuery query)
        {
            var role = await _unitOfWork.RolesRepository.GetAsync(query.Id);

            if (role == null)
            {
                throw new NullReferenceException("Role does not exist!");
            }

            return _mapper.Map<RoleDto>(role);
        }
    }
}
