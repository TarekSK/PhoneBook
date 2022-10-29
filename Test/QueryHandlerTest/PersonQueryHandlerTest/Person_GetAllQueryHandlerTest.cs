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
    [Collection("GetAllPersonQueryHandler")]
    public class Person_GetAllQueryHandlerTest
    {
        private readonly GetAllPersonQueryHandler _getAllPersonQueryHandler;
        private readonly Mock<IPersonRepository> _PersonRepositoryMock = new();

        public Person_GetAllQueryHandlerTest()
        {
            _getAllPersonQueryHandler = new GetAllPersonQueryHandler(_PersonRepositoryMock.Object);
        }

        [Fact]
        [Trait("GetAllPersonQuery", "Positive")]
        public async Task GetAllPersonQuery_Success()
        {
            // Arrange
            ServiceResponse<List<Person>> expectedPersonGetAllResponse = 
                new(HttpStatusCode.OK, PersonTestData.PersonDefaultData());

            _PersonRepositoryMock.Setup(PersonRepo => 
            PersonRepo.PersonGetAll()).ReturnsAsync(expectedPersonGetAllResponse);

            // Act
            var actualGetAllPersonResponse = await _getAllPersonQueryHandler.Handle(new GetAllPersonQuery(), default);

            // Assert
            actualGetAllPersonResponse.StatusCode.Should().Be(HttpStatusCode.OK);
            actualGetAllPersonResponse.Errors.Should().BeEmpty();
            actualGetAllPersonResponse.Data.Should().Contain(expectedPersonGetAllResponse.Data);
        }
    }
}
