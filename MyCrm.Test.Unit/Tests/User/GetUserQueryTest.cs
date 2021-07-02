using System.Threading.Tasks;
using AutoMapper;
using FluentAssertions;
using MyCrm.Domain;
using MyCrm.Domain.Query.User;
using MyCrm.Domain.Repositories;
using NSubstitute;
using Xunit;

namespace MyCrm.Test.Unit.Tests.User
{
    public class GetUserQueryTest
    {
        [Fact]
        public async Task GetUser_WhenItExists_ReturnCorrectData()
        {
            using (var sut = new SystemUnderTest())
            {
                var User = sut.CreateUser();
                var unitOfWorkSubstitute = Substitute.For<IUnitOfWork>();

                unitOfWorkSubstitute.UsersRepository.GetAsync(User.Id).Returns(User);

                var query = new GetUserQuery(User.Id);
                var mapper = new Mapper(new MapperConfiguration(cfg => cfg.AddProfile(new EntityMappingProfile())));
                var handler = new GetUserQueryHandler(unitOfWorkSubstitute, mapper);
                var UserQuery = await handler.HandleAsync(query);

                UserQuery.Id.Should().Be(User.Id);
            }
        }
    }
}
