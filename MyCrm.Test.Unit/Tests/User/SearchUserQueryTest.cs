using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using FluentAssertions;
using MyCrm.Domain;
using MyCrm.Domain.Entities;
using MyCrm.Domain.Enums;
using MyCrm.Domain.Query.User;
using MyCrm.Domain.Query.Dto.Pagination.PageResults;
using MyCrm.Domain.Repositories;
using NSubstitute;
using Xunit;

namespace MyCrm.Test.Unit.Tests.User
{
    public class SearchUserQueryTest
    {
        [Fact]
        public async Task SearchUser_WhenItExists_ReturnCorrectData()
        {
            using (var sut = new SystemUnderTest())
            {
                var user = sut.CreateUser();
                var users = new List<Domain.Entities.User>() { user };
                var pageResult = new UserPageResult<Domain.Entities.User>(users, 1, 10, 1);
                var unitOfWorkSubstitute = Substitute.For<IUnitOfWork>();

                var query = new SearchUsersQuery()
                {
                    SearchPhrase = user.FirstName,
                    PageNumber = 1,
                    PageSize = 10,
                    OrderBy = "FirstName",
                    SortDirection = SortDirection.DESC
                };

                unitOfWorkSubstitute
                    .UsersRepository
                    .SearchAsync(query.SearchPhrase, query.PageNumber, query.PageSize, query.OrderBy, query.SortDirection)
                    .Returns(pageResult);

                var mapper = new Mapper(new MapperConfiguration(cfg => cfg.AddProfile(new EntityMappingProfile())));
                var handler = new SearchUsersQueryHandler(unitOfWorkSubstitute, mapper);
                var UserQuery = await handler.HandleAsync(query);

                foreach (var usr in UserQuery.Items)
                {
                    usr.Id.Should().Be(user.Id);
                }
            }
        }
    }
}
