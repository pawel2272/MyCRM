using System;
using System.Linq;
using System.Threading.Tasks;
using FluentAssertions;
using MyCrm.Domain.Command.User;
using MyCrm.Domain.Entities;
using MyCrm.Domain.Repositories;
using NSubstitute;
using NSubstitute.ReturnsExtensions;
using Xunit;

namespace MyCrm.Test.Unit.Tests.User
{
    public class DeleteUserCommandTest
    {
        [Fact]
        public async Task DeleteUser_WhenItIsPossible_ShouldSuccess()
        {
            using (var sut = new SystemUnderTest())
            {
                Guid guid = Guid.NewGuid();

                var command = new DeleteUserCommand(guid);

                var unitOfWorkSubstitute = Substitute.For<IUnitOfWork>();

                unitOfWorkSubstitute.UsersRepository.GetAsync(guid).Returns(new Domain.Entities.User());

                var handler = new DeleteUserCommandHandler(unitOfWorkSubstitute);

                var result = await handler.HandleAsync(command);

                result.IsSuccess.Should().Be(true);
            }
        }

        [Fact]
        public async Task DeleteUser_WhenItIsPossible_ShouldFail()
        {
            using (var sut = new SystemUnderTest())
            {
                var command = new DeleteUserCommand(Guid.Empty);

                var unitOfWorkSubstitute = Substitute.For<IUnitOfWork>();

                unitOfWorkSubstitute.UsersRepository.GetAsync(Guid.Empty).ReturnsNull();

                var handler = new DeleteUserCommandHandler(unitOfWorkSubstitute);

                var result = await handler.HandleAsync(command);

                result.IsSuccess.Should().Be(false);

                result.Errors.Count().Should().Be(0);

                result.Message.Should().Be("User does not exist.");
            }
        }
    }
}
