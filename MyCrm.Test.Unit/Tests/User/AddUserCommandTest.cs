using System;
using System.Linq;
using AutoMapper;
using FluentAssertions;
using MyCrm.Domain.Command.User;
using MyCrm.Domain.Repositories;
using NSubstitute;
using System.Threading.Tasks;
using MyCrm.Domain;
using Xunit;

namespace MyCrm.Test.Unit.Tests.User
{
    public class AddUserCommandTest
    {
        [Fact]
        public async Task AddUser_WhenItIsPossible_ShouldSuccess()
        {
            using (var sut = new SystemUnderTest())
            {
                var command = new AddUserCommand()
                {
                    Username = "admin",
                    Gender = "m",
                    Password = "adminadminadmin",
                    FirstName = "Jan",
                    LastName = "Kowalski",
                    Phone = "123456789",
                    Email = "jan@kowalski.pl",
                    Street = "Miodowa 12",
                    PostalCode = "00-000",
                    City = "Warszawa",
                    RoleId = Guid.NewGuid()
                };

                var unitOfWorkSubstitute = Substitute.For<IUnitOfWork>();

                var mapper = new Mapper(new MapperConfiguration(cfg => cfg.AddProfile(new EntityMappingProfile())));

                var handler = new AddUserCommandHandler(unitOfWorkSubstitute, mapper);

                var result = await handler.HandleAsync(command);

                result.IsSuccess.Should().Be(true);
            }
        }

        [Fact]
        public async Task AddUser_WhenItIsPossible_ShouldFail()
        {
            using (var sut = new SystemUnderTest())
            {
                var command = new AddUserCommand()
                {
                    Username = "",
                    Gender = "",
                    Password = "",
                    FirstName = "",
                    LastName = "",
                    Phone = "",
                    Email = "",
                    Street = "",
                    PostalCode = "",
                    City = "",
                    RoleId = Guid.Empty
                };

                var unitOfWorkSubstitute = Substitute.For<IUnitOfWork>();

                var mapper = new Mapper(new MapperConfiguration(cfg => cfg.AddProfile(new EntityMappingProfile())));

                var handler = new AddUserCommandHandler(unitOfWorkSubstitute, mapper);

                var result = await handler.HandleAsync(command);

                result.IsSuccess.Should().Be(false);

                result.Errors.Count().Should().Be(13);
            }
        }
    }
}
