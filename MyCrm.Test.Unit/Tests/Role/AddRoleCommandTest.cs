using System;
using System.Linq;
using AutoMapper;
using FluentAssertions;
using MyCrm.Domain.Command.Contact;
using MyCrm.Domain.Repositories;
using NSubstitute;
using System.Threading.Tasks;
using MyCrm.Domain;
using MyCrm.Domain.Command.Role;
using Xunit;

namespace MyCrm.Test.Unit.Tests.Role
{
    public class AddRoleCommandTest
    {
        [Fact]
        public async Task AddRole_WhenItIsPossible_ShouldSuccess()
        {
            using (var sut = new SystemUnderTest())
            {
                var command = new AddRoleCommand()
                {
                    Name = "Sample"
                };

                var unitOfWorkSubstitute = Substitute.For<IUnitOfWork>();

                var mapper = new Mapper(new MapperConfiguration(cfg => cfg.AddProfile(new EntityMappingProfile())));

                var handler = new AddRoleCommandHandler(unitOfWorkSubstitute, mapper);

                var result = await handler.HandleAsync(command);

                result.IsSuccess.Should().Be(true);
            }
        }

        [Fact]
        public async Task AddRole_WhenItIsPossible_ShouldFail()
        {
            using (var sut = new SystemUnderTest())
            {
                var command = new AddRoleCommand()
                {
                    Name = ""
                };

                var unitOfWorkSubstitute = Substitute.For<IUnitOfWork>();

                var mapper = new Mapper(new MapperConfiguration(cfg => cfg.AddProfile(new EntityMappingProfile())));

                var handler = new AddRoleCommandHandler(unitOfWorkSubstitute, mapper);

                var result = await handler.HandleAsync(command);

                result.IsSuccess.Should().Be(false);

                result.Errors.Count().Should().Be(1);
            }
        }
    }
}
