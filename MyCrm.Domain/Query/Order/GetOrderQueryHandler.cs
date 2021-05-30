using System;
using System.Threading.Tasks;
using AutoMapper;
using MyCrm.Domain.Query.Dto;
using MyCrm.Domain.Repositories;

namespace MyCrm.Domain.Query.Order
{
    public sealed class GetOrderQueryHandler : IQueryHandler<GetOrderQuery, OrderDto>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public GetOrderQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }

        public async Task<OrderDto> HandleAsync(GetOrderQuery query)
        {
            var order = await _unitOfWork.OrdersRepository.GetAsync(query.Id);

            if (order == null)
            {
                throw new NullReferenceException("Order does not exist!");
            }

            return _mapper.Map<OrderDto>(order);
        }
    }
}
