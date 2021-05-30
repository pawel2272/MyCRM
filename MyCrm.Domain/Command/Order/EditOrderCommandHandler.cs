using System;
using System.Threading.Tasks;
using AutoMapper;
using MyCrm.Domain.Repositories;

namespace MyCrm.Domain.Command.Order
{
    public sealed class EditOrderCommandHandler : ICommandHandler<EditOrderCommand>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public EditOrderCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }

        public async Task<Result> HandleAsync(EditOrderCommand command)
        {
            var validationResult = await new EditOrderCommandValidator().ValidateAsync(command);
            if (!validationResult.IsValid)
            {
                return Result.Fail(validationResult);
            }

            var order = await _unitOfWork.OrdersRepository.GetAsync(command.Id);
            if (order == null)
            {
                return Result.Fail("Order does not exist.");
            }

            _mapper.Map(command, order);

            order.ModDate = DateTime.Now;

            await _unitOfWork.OrdersRepository.UpdateAsync(order);
            await _unitOfWork.CommitAsync();

            return Result.Ok();
        }
    }
}
