using System;
using System.Linq;
using AutoMapper;
using FluentAssertions;
using MyCrm.Domain.Command.Contact;
using MyCrm.Domain.Repositories;
using NSubstitute;
using System.Threading.Tasks;
using MyCrm.Domain;
using MyCrm.Domain.Command.Order;
using Xunit;

namespace MyCrm.Test.Unit.Tests.Order
{
    public class AddOrderCommandTest
    {
        [Fact]
        public async Task AddOrder_WhenItIsPossible_ShouldSuccess()
        {
            using (var sut = new SystemUnderTest())
            {
                var command = new AddOrderCommand()
                {
                    Title = "Test",
                    Content = "Test",
                    ContactId = Guid.NewGuid(),
                    Price = 3.14m
                };

                var unitOfWorkSubstitute = Substitute.For<IUnitOfWork>();

                var mapper = new Mapper(new MapperConfiguration(cfg => cfg.AddProfile(new EntityMappingProfile())));

                var handler = new AddOrderCommandHandler(unitOfWorkSubstitute, mapper);

                var result = await handler.HandleAsync(command);

                result.IsSuccess.Should().Be(true);
            }
        }

        [Fact]
        public async Task AddOrder_WhenItIsPossible_ShouldFail()
        {
            using (var sut = new SystemUnderTest())
            {
                var command = new AddOrderCommand()
                {
                    Title = "",
                    Content = "",
                    ContactId = Guid.Empty,
                    Price = 0
                };

                var unitOfWorkSubstitute = Substitute.For<IUnitOfWork>();

                var mapper = new Mapper(new MapperConfiguration(cfg => cfg.AddProfile(new EntityMappingProfile())));

                var handler = new AddOrderCommandHandler(unitOfWorkSubstitute, mapper);

                var result = await handler.HandleAsync(command);

                result.IsSuccess.Should().Be(false);

                result.Errors.Count().Should().Be(3);
            }
        }
    }
}
