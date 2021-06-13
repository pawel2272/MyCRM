﻿using System;
using System.Linq;
using System.Threading.Tasks;
using FluentAssertions;
using MyCrm.Domain.Command.Contact;
using MyCrm.Domain.Command.Order;
using MyCrm.Domain.Entities;
using MyCrm.Domain.Repositories;
using NSubstitute;
using NSubstitute.ReturnsExtensions;
using Xunit;

namespace MyCrm.Test.Unit.Tests.Order
{
    public class DeleteOrderCommandTest
    {
        [Fact]
        public async Task DeleteOrder_WhenItIsPossible_ShouldSuccess()
        {
            using (var sut = new SystemUnderTest())
            {
                Guid guid = Guid.NewGuid();

                var command = new DeleteOrderCommand(guid);

                var unitOfWorkSubstitute = Substitute.For<IUnitOfWork>();

                unitOfWorkSubstitute.OrdersRepository.GetAsync(guid).Returns(new Domain.Entities.Order());

                var handler = new DeleteOrderCommandHandler(unitOfWorkSubstitute);

                var result = await handler.HandleAsync(command);

                result.IsSuccess.Should().Be(true);
            }
        }

        [Fact]
        public async Task DeleteOrder_WhenItIsPossible_ShouldFail()
        {
            using (var sut = new SystemUnderTest())
            {
                var command = new DeleteOrderCommand(Guid.Empty);

                var unitOfWorkSubstitute = Substitute.For<IUnitOfWork>();

                unitOfWorkSubstitute.ContactsRepository.GetAsync(Guid.Empty).ReturnsNull();

                var handler = new DeleteOrderCommandHandler(unitOfWorkSubstitute);

                var result = await handler.HandleAsync(command);

                result.IsSuccess.Should().Be(false);

                result.Errors.Count().Should().Be(0);

                result.Message.Should().Be("Order does not exist.");
            }
        }
    }
}
