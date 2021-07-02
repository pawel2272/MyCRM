using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using FluentAssertions;
using MyCrm.Domain;
using MyCrm.Domain.Entities;
using MyCrm.Domain.Enums;
using MyCrm.Domain.Query.Todo;
using MyCrm.Domain.Query.Dto.Pagination.PageResults;
using MyCrm.Domain.Repositories;
using NSubstitute;
using Xunit;

namespace MyCrm.Test.Unit.Tests.Todo
{
    public class SearchTodoQueryTest
    {
        [Fact]
        public async Task SearchTodo_WhenItExists_ReturnCorrectData()
        {
            using (var sut = new SystemUnderTest())
            {
                var todo = sut.CreateTodo();
                var todos = new List<Domain.Entities.Todo>() { todo };
                var pageResult = new TodoPageResult<Domain.Entities.Todo>(todos, 1, 10, 1, todo.ContactId);
                var unitOfWorkSubstitute = Substitute.For<IUnitOfWork>();

                var query = new SearchTodosQuery()
                {
                    ContactId = todo.ContactId,
                    SearchPhrase = todo.Title,
                    PageNumber = 1,
                    PageSize = 10,
                    OrderBy = "Title",
                    SortDirection = SortDirection.DESC
                };

                unitOfWorkSubstitute
                    .TodosRepository
                    .SearchAsync(query.ContactId, query.SearchPhrase, query.PageNumber, query.PageSize, query.OrderBy, query.SortDirection)
                    .Returns(pageResult);

                var mapper = new Mapper(new MapperConfiguration(cfg => cfg.AddProfile(new EntityMappingProfile())));
                var handler = new SearchTodosQueryHandler(unitOfWorkSubstitute, mapper);
                var todoQuery = await handler.HandleAsync(query);

                foreach (var tod in todoQuery.Items)
                {
                    tod.Id.Should().Be(todo.Id);
                }
            }
        }
    }
}
