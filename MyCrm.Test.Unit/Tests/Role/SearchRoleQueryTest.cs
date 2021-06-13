using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using FluentAssertions;
using MyCrm.Domain;
using MyCrm.Domain.Entities;
using MyCrm.Domain.Enums;
using MyCrm.Domain.Query.Contact;
using MyCrm.Domain.Query.Dto.Pagination.PageResults;
using MyCrm.Domain.Repositories;
using NSubstitute;
using Xunit;

namespace MyCrm.Test.Unit.Tests.Role
{
    public class SearchRoleQueryTest
    {
        [Fact]
        public async Task SearchContact_WhenItExists_ReturnCorrectData()
        {
            using (var sut = new SystemUnderTest())
            {
                var contact = sut.CreateContact();
                var contacts = new List<Domain.Entities.Contact>() { contact };
                var pageResult = new ContactPageResult<Domain.Entities.Contact>(contacts, 1, 10, 1);
                var unitOfWorkSubstitute = Substitute.For<IUnitOfWork>();

                var query = new SearchContactsQuery()
                {
                    UserId = contact.UserId,
                    SearchPhrase = contact.FirstName,
                    PageNumber = 1,
                    PageSize = 10,
                    OrderBy = "FirstName",
                    SortDirection = SortDirection.DESC
                };

                unitOfWorkSubstitute
                    .ContactsRepository
                    .SearchAsync(query.UserId, query.SearchPhrase, query.PageNumber, query.PageSize, query.OrderBy, query.SortDirection)
                    .Returns(pageResult);

                var mapper = new Mapper(new MapperConfiguration(cfg => cfg.AddProfile(new EntityMappingProfile())));
                var handler = new SearchContactsQueryHandler(unitOfWorkSubstitute, mapper);
                var contactQuery = await handler.HandleAsync(query);

                foreach (var cntct in contactQuery.Items)
                {
                    cntct.Id.Should().Be(contact.Id);
                }
            }
        }
    }
}
