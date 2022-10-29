using Application.Query.Person;
using Domain.Helper;
using Domain.Models;
using FluentAssertions;
using Moq;
using Persistence.Interface;
using PhoneBook.UnitTest.TestHelper;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using Xunit;

namespace PhoneBook.UnitTest.QueryHandlerTest.PersonQueryHandlerTest
{
    [Collection("SearchPersonQueryHandler")]
    public class Person_SearchQueryHandlerTest
    {
        private readonly SearchPersonQueryHandler _SearchPersonQueryHandler;
        private readonly Mock<IPersonRepository> _PersonRepositoryMock = new();

        public Person_SearchQueryHandlerTest()
        {
            _SearchPersonQueryHandler = new SearchPersonQueryHandler(_PersonRepositoryMock.Object);
        }

        [Theory]
        [InlineData("Person - 001")]
        [InlineData("Person - 002")]
        [Trait("SearchPersonQuery", "Positive")]
        public async Task SearchPersonQuery_Success(string searchText)
        {
            // Arrange
            ServiceResponse<List<Person>> expectedPersonSearchResponse =
                new(HttpStatusCode.OK, PersonTestData.PersonDefaultData());

            _PersonRepositoryMock.Setup(PersonRepo =>
            PersonRepo.PersonSearch(searchText)).ReturnsAsync(expectedPersonSearchResponse);

            // Act
            var actualSearchPersonResponse = 
                await _SearchPersonQueryHandler.Handle(new SearchPersonQuery(searchText), default);

            // Assert
            actualSearchPersonResponse.StatusCode.Should().Be(HttpStatusCode.OK);
            actualSearchPersonResponse.Errors.Should().BeEmpty();
            actualSearchPersonResponse.Data.Should().Contain(expectedPersonSearchResponse.Data);
        }
    }
}
