using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using FluentAssertions;
using MyCrm.Domain;
using MyCrm.Domain.Entities;
using MyCrm.Domain.Enums;
using MyCrm.Domain.Query.Contact;
using MyCrm.Domain.Query.Dto.Pagination.PageResults;
using MyCrm.Domain.Query.Order;
using MyCrm.Domain.Repositories;
using NSubstitute;
using Xunit;

namespace MyCrm.Test.Unit.Tests.Order
{
    public class SearchOrderQueryTest
    {
        [Fact]
        public async Task SearchOrder_WhenItExists_ReturnCorrectData()
        {
            using (var sut = new SystemUnderTest())
            {
                var order = sut.CreateOrder();
                var orders = new List<Domain.Entities.Order>() { order };
                var pageResult = new OrderPageResult<Domain.Entities.Order>(orders, 1, 10, 1, order.ContactId);
                var unitOfWorkSubstitute = Substitute.For<IUnitOfWork>();

                var query = new SearchOrdersQuery()
                {
                    ContactId = order.ContactId,
                    SearchPhrase = order.Title,
                    PageNumber = 1,
                    PageSize = 10,
                    OrderBy = "Title",
                    SortDirection = SortDirection.DESC
                };

                unitOfWorkSubstitute
                    .OrdersRepository
                    .SearchAsync(query.ContactId, query.SearchPhrase, query.PageNumber, query.PageSize, query.OrderBy, query.SortDirection)
                    .Returns(pageResult);

                var mapper = new Mapper(new MapperConfiguration(cfg => cfg.AddProfile(new EntityMappingProfile())));
                var handler = new SearchOrdersQueryHandler(unitOfWorkSubstitute, mapper);
                var orderQuery = await handler.HandleAsync(query);

                foreach (var ordr in orderQuery.Items)
                {
                    ordr.Id.Should().Be(order.Id);
                }
            }
        }
    }
}
