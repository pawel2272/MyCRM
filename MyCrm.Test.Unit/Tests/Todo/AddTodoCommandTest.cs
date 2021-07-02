using System;
using System.Linq;
using AutoMapper;
using FluentAssertions;
using MyCrm.Domain.Command.Todo;
using MyCrm.Domain.Repositories;
using NSubstitute;
using System.Threading.Tasks;
using MyCrm.Domain;
using Xunit;

namespace MyCrm.Test.Unit.Tests.Todo
{
    public class AddTodoCommandTest
    {
        [Fact]
        public async Task AddTodo_WhenItIsPossible_ShouldSuccess()
        {
            using (var sut = new SystemUnderTest())
            {
                var command = new AddTodoCommand()
                {
                    Title = "Tit",
                    Content = "Con",
                    ContactId = Guid.NewGuid()
                };

                var unitOfWorkSubstitute = Substitute.For<IUnitOfWork>();

                var mapper = new Mapper(new MapperConfiguration(cfg => cfg.AddProfile(new EntityMappingProfile())));

                var handler = new AddTodoCommandHandler(unitOfWorkSubstitute, mapper);

                var result = await handler.HandleAsync(command);

                result.IsSuccess.Should().Be(true);
            }
        }

        [Fact]
        public async Task AddTodo_WhenItIsPossible_ShouldFail()
        {
            using (var sut = new SystemUnderTest())
            {
                var command = new AddTodoCommand()
                {
                    Title = "",
                    Content = "",
                    ContactId = Guid.Empty
                };

                var unitOfWorkSubstitute = Substitute.For<IUnitOfWork>();

                var mapper = new Mapper(new MapperConfiguration(cfg => cfg.AddProfile(new EntityMappingProfile())));

                var handler = new AddTodoCommandHandler(unitOfWorkSubstitute, mapper);

                var result = await handler.HandleAsync(command);

                result.IsSuccess.Should().Be(false);

                result.Errors.Count().Should().Be(2);
            }
        }
    }
}
