using System;
using System.Linq;
using System.Threading.Tasks;
using FluentAssertions;
using MyCrm.Domain.Command.Todo;
using MyCrm.Domain.Entities;
using MyCrm.Domain.Repositories;
using NSubstitute;
using NSubstitute.ReturnsExtensions;
using Xunit;

namespace MyCrm.Test.Unit.Tests.Todo
{
    public class DeleteTodoCommandTest
    {
        [Fact]
        public async Task DeleteTodo_WhenItIsPossible_ShouldSuccess()
        {
            using (var sut = new SystemUnderTest())
            {
                Guid guid = Guid.NewGuid();

                var command = new DeleteTodoCommand(guid, guid);

                var unitOfWorkSubstitute = Substitute.For<IUnitOfWork>();

                unitOfWorkSubstitute.TodosRepository.GetAsync(guid).Returns(new Domain.Entities.Todo());

                var handler = new DeleteTodoCommandHandler(unitOfWorkSubstitute);

                var result = await handler.HandleAsync(command);

                result.IsSuccess.Should().Be(true);
            }
        }

        [Fact]
        public async Task DeleteTodo_WhenItIsPossible_ShouldFail()
        {
            using (var sut = new SystemUnderTest())
            {
                var command = new DeleteTodoCommand(Guid.Empty, Guid.Empty);

                var unitOfWorkSubstitute = Substitute.For<IUnitOfWork>();

                unitOfWorkSubstitute.TodosRepository.GetAsync(Guid.Empty).ReturnsNull();

                var handler = new DeleteTodoCommandHandler(unitOfWorkSubstitute);

                var result = await handler.HandleAsync(command);

                result.IsSuccess.Should().Be(false);

                result.Errors.Count().Should().Be(0);

                result.Message.Should().Be("Todo does not exist.");
            }
        }
    }
}
