using System;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using FluentAssertions;
using MyCrm.Domain;
using MyCrm.Domain.Command.Todo;
using MyCrm.Domain.Entities;
using MyCrm.Domain.Repositories;
using NSubstitute;
using Xunit;

namespace MyCrm.Test.Unit.Tests.Todo
{
    public class EditTodoCommandTest
    {
        [Fact]
        public async Task EditTodo_WhenItIsPossible_ShouldSuccess()
        {
            using (var sut = new SystemUnderTest())
            {
                var command = new EditTodoCommand()
                {
                    Id = Guid.NewGuid(),
                    Content = "cCon",
                    Title = "ntent0",
                    ContactId = Guid.NewGuid()
                };

                var unitOfWorkSubstitute = Substitute.For<IUnitOfWork>();

                unitOfWorkSubstitute.TodosRepository.GetAsync(command.Id).Returns(new Domain.Entities.Todo());

                var mapper = new Mapper(new MapperConfiguration(cfg => cfg.AddProfile(new EntityMappingProfile())));

                var handler = new EditTodoCommandHandler(unitOfWorkSubstitute, mapper);

                var result = await handler.HandleAsync(command);

                result.IsSuccess.Should().Be(true);
            }
        }

        [Fact]
        public async Task EditTodo_WhenItIsPossible_ShouldFail()
        {
            using (var sut = new SystemUnderTest())
            {
                var command = new EditTodoCommand()
                {
                    Id = Guid.Empty,
                    Content = "",
                    Title = "",
                    ContactId = Guid.Empty
                };

                var unitOfWorkSubstitute = Substitute.For<IUnitOfWork>();

                unitOfWorkSubstitute.TodosRepository.GetAsync(command.Id).Returns(new Domain.Entities.Todo());

                var mapper = new Mapper(new MapperConfiguration(cfg => cfg.AddProfile(new EntityMappingProfile())));

                var handler = new EditTodoCommandHandler(unitOfWorkSubstitute, mapper);

                var result = await handler.HandleAsync(command);

                result.IsSuccess.Should().Be(false);

                result.Errors.Count().Should().Be(3);
            }
        }
    }
}
