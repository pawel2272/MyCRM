using System;
using AutoMapper;
using FluentAssertions;
using MyCrm.Domain.Command.Contact;
using MyCrm.Domain.Repositories;
using NSubstitute;
using System.Threading.Tasks;
using Xunit;

namespace MyCrm.Test.Unit
{
    public class AddContactCommandTest
    {
        [Fact]
        public async Task AddContact_WhenItIsPossible_ShouldSuccess()
        {
            using (var sut = new SystemUnderTest())
            {
                var command = new AddContactCommand()
                {
                    FirstName = "Jan",
                    LastName = "Kowalski",
                    Phone = "123456789",
                    Email = "jan@kowalski.pl",
                    Street = "Miodowa 12",
                    PostalCode = "00-000",
                    City = "Warszawa",
                    ContactComment = "Sample comment",
                    UserId = Guid.Empty
                };

                var unitOfWorkSubstitute = Substitute.For<IUnitOfWork>();

                var mapperSubstitute = Substitute.For<IMapper>();

                var handler = new AddContactCommandHandler(unitOfWorkSubstitute, mapperSubstitute);

                var result = await handler.HandleAsync(command);

                result.IsSuccess.Should().Be(true);
            }
        }
    }
}
