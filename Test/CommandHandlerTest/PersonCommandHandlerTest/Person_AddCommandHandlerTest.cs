using Application.Command.Person;
using Domain.Helper;
using Domain.Models;
using FluentAssertions;
using Moq;
using Persistence.Interface;
using PhoneBook.UnitTest.TestHelper;
using System;
using System.Net;
using System.Threading.Tasks;
using Xunit;

namespace PhoneBook.UnitTest.CommandHandlerTest.PersonCommandHandlerTest
{
    [Collection("AddPersonCommandHandler")]
    public class Person_AddCommandHandlerTest
    {
        private readonly AddPersonCommandHandler _addPersonCommandHandler;
        private readonly Mock<IPersonRepository> _PersonRepositoryMock = new();

        public Person_AddCommandHandlerTest()
        {
            _addPersonCommandHandler = new AddPersonCommandHandler(_PersonRepositoryMock.Object);
        }

        [Fact]
        [Trait("AddPersonCommand", "Positive")]
        public async Task AddPersonCommand_Success()
        {
            // Arrange
            var expectedPersonAddResponse = new ServiceResponse<Person>(HttpStatusCode.Created, new Person());
            _PersonRepositoryMock.Setup(PersonRepo => 
            PersonRepo.PersonAdd(It.IsAny<Person>())).ReturnsAsync(expectedPersonAddResponse);

            // Act
            var actualAddPersonResponse = 
                await _addPersonCommandHandler.Handle(new AddPersonCommand(PersonTestData.ExpectedPersonData()), default);

            // Assert
            actualAddPersonResponse.StatusCode.Should().Be(HttpStatusCode.Created);
            actualAddPersonResponse.Errors.Should().BeEmpty();
        }

        [Fact]
        [Trait("AddPersonCommand", "Negative")]
        public async Task AddPersonCommand_Error()
        {
            // Arrange
            _PersonRepositoryMock.Setup(PersonRepo => 
            PersonRepo.PersonAdd(It.IsAny<Person>())).ThrowsAsync(new Exception());

            // Act
            var actualAddPersonResponse = 
                await _addPersonCommandHandler.Handle(new AddPersonCommand(PersonTestData.ExpectedPersonData()), default);

            // Assert
            actualAddPersonResponse.StatusCode.Should().Be(HttpStatusCode.InternalServerError);
            actualAddPersonResponse.Errors.Should().Contain("Unexpected Error Has Occured");
        }
    }
}
