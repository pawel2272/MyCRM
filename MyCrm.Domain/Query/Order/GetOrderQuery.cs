using MyCrm.Domain.Query.Dto;
using System;

namespace MyCrm.Domain.Query.Order
{
    public sealed class GetOrderQuery : IQuery<OrderDto>
    {
        public GetOrderQuery(Guid id)
        {
            Id = id;
        }

        public Guid Id { get; }
    }
}
