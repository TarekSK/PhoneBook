using Application.Query.Person;
using Domain.Helper;
using Domain.Models;
using FluentAssertions;
using Moq;
using Persistence.Interface;
using PhoneBook.UnitTest.TestHelper;
using System.Net;
using System.Threading.Tasks;
using Xunit;

namespace PhoneBook.UnitTest.QueryHandlerTest.PersonQueryHandlerTest
{
    [Collection("WildCardPersonQueryHandler")]
    public class Person_WildCardQueryHandlerTest
    {
        private readonly WildCardPersonQueryHandler _WildCardPersonQueryHandler;
        private readonly Mock<IPersonRepository> _PersonRepositoryMock = new();

        public Person_WildCardQueryHandlerTest()
        {
            _WildCardPersonQueryHandler = new WildCardPersonQueryHandler(_PersonRepositoryMock.Object);
        }

        [Fact]
        [Trait("WildCardPersonQuery", "Positive")]
        public async Task WildCardPersonQuery_Success()
        {
            // Arrange
            ServiceResponse<Person> expectedPersonWildCardResponse =
                new(HttpStatusCode.OK, PersonTestData.ExpectedPersonData());

            _PersonRepositoryMock.Setup(PersonRepo =>
            PersonRepo.PersonWildCard()).ReturnsAsync(expectedPersonWildCardResponse);

            // Act
            var actualWildCardPersonResponse = 
                await _WildCardPersonQueryHandler.Handle(new WildCardPersonQuery(), default);

            // Assert
            actualWildCardPersonResponse.StatusCode.Should().Be(HttpStatusCode.OK);
            actualWildCardPersonResponse.Errors.Should().BeEmpty();
            actualWildCardPersonResponse.Data.Should().BeOfType<Person>();
        }
    }
}
