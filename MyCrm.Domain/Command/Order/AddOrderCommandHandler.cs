using System;
using System.Threading.Tasks;
using AutoMapper;
using MyCrm.Domain.Repositories;

namespace MyCrm.Domain.Command.Order
{
    public sealed class AddOrderCommandHandler : ICommandHandler<AddOrderCommand>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public AddOrderCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }

        public async Task<Result> HandleAsync(AddOrderCommand command)
        {
            var validationResult = await new AddOrderCommandValidator().ValidateAsync(command);

            if (!validationResult.IsValid)
            {
                return Result.Fail(validationResult);
            }

            var order = _mapper.Map<Entities.Order>(command);
            order.Id = Guid.NewGuid();
            order.ModDate = DateTime.Now;
            order.CreDate = DateTime.Now;
            await _unitOfWork.OrdersRepository.AddAsync(order);
            await _unitOfWork.CommitAsync();

            return Result.Ok();
        }
    }
}
