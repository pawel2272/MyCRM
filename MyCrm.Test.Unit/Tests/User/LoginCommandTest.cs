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
    public class LoginCommandTest
    {
        [Fact]
        public async Task Login_WhenItIsPossible_ShouldSuccess()
        {
            using (var sut = new SystemUnderTest())
            {
                var user = sut.CreateUser();

                var command = new LoginCommand()
                {
                    Username = "admin",
                    Password = "password",
                    RememberMe = true
                };

                var unitOfWorkSubstitute = Substitute.For<IUnitOfWork>();

                unitOfWorkSubstitute.UsersRepository.GetAsync(user.Id).Returns(user);
                unitOfWorkSubstitute.UsersRepository.LoginAsync(command.Username, command.Password, command.RememberMe).Returns("asd");

                var handler = new LoginCommandHandler(unitOfWorkSubstitute);

                var result = await handler.HandleAsync(command);

                result.IsSuccess.Should().Be(true);
            }
        }

        [Fact]
        public async Task Login_WhenItIsPossible_ShouldFail()
        {
            using (var sut = new SystemUnderTest())
            {
                var user = sut.CreateUser();

                var command = new LoginCommand()
                {
                    Username = "",
                    Password = "",
                    RememberMe = true
                };

                var unitOfWorkSubstitute = Substitute.For<IUnitOfWork>();

                unitOfWorkSubstitute.UsersRepository.GetAsync(user.Id).Returns(user);
                unitOfWorkSubstitute.UsersRepository.LoginAsync(command.Username, command.Password, command.RememberMe).Returns("asd");

                var handler = new LoginCommandHandler(unitOfWorkSubstitute);

                var result = await handler.HandleAsync(command);

                result.IsSuccess.Should().Be(false);

                result.Errors.Count().Should().Be(2);
            }
        }
    }
}
