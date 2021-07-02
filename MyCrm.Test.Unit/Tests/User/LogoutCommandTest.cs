using System;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using FluentAssertions;
using MyCrm.Domain;
using MyCrm.Domain.Command.Contact;
using MyCrm.Domain.Command.User;
using MyCrm.Domain.Entities;
using MyCrm.Domain.Repositories;
using NSubstitute;
using Xunit;

namespace MyCrm.Test.Unit.Tests.User
{
    public class LogoutCommandTest
    {
        [Fact]
        public async Task Logout_WhenItIsPossible_ShouldSuccess()
        {
            using (var sut = new SystemUnderTest())
            {
                var user = sut.CreateUser();

                var command = new LogoutCommand();

                var unitOfWorkSubstitute = Substitute.For<IUnitOfWork>();

                unitOfWorkSubstitute.UsersRepository.GetAsync(user.Id).Returns(user);

                var handler = new LogoutCommandHandler(unitOfWorkSubstitute);

                var result = await handler.HandleAsync(command);

                result.IsSuccess.Should().Be(true);
            }
        }
    }
}
