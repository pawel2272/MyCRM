using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using FluentAssertions;
using MyCrm.Domain;
using MyCrm.Domain.Entities;
using MyCrm.Domain.Enums;
using MyCrm.Domain.Query.Contact;
using MyCrm.Domain.Query.Dto.Pagination.PageResults;
using MyCrm.Domain.Query.Role;
using MyCrm.Domain.Repositories;
using NSubstitute;
using Xunit;

namespace MyCrm.Test.Unit.Tests.Role
{
    public class SearchRoleQueryTest
    {
        [Fact]
        public async Task SearchRole_WhenItExists_ReturnCorrectData()
        {
            using (var sut = new SystemUnderTest())
            {
                var role = sut.CreateRole();
                var roles = new List<Domain.Entities.Role>() { role };
                var pageResult = new RolePageResult<Domain.Entities.Role>(roles, 1, 10, 1);
                var unitOfWorkSubstitute = Substitute.For<IUnitOfWork>();

                var query = new SearchRolesQuery()
                {
                    SearchPhrase = role.Name,
                    PageNumber = 1,
                    PageSize = 10,
                    OrderBy = "Name",
                    SortDirection = SortDirection.DESC
                };

                unitOfWorkSubstitute
                    .RolesRepository
                    .SearchAsync(query.SearchPhrase, query.PageNumber, query.PageSize, query.OrderBy, query.SortDirection)
                    .Returns(pageResult);

                var mapper = new Mapper(new MapperConfiguration(cfg => cfg.AddProfile(new EntityMappingProfile())));
                var handler = new SearchRolesQueryHandler(unitOfWorkSubstitute, mapper);
                var roleQuery = await handler.HandleAsync(query);

                foreach (var rolee in roleQuery.Items)
                {
                    rolee.Id.Should().Be(role.Id);
                }
            }
        }
    }
}
