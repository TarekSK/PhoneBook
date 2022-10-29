using Application.Command.Person;
using Domain.Helper;
using Domain.Models;
using FluentAssertions;
using Moq;
using Persistence.Interface;
using System;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using Xunit;

namespace PhoneBook.UnitTest.CommandHandlerTest.PersonCommandHandlerTest
{
    [Collection("AddEditRemovePersonCommandHandler")]
    public class Person_Add_Edit_Remove_CommandHandlerTest
    {
        private readonly AddEditRemovePersonCommandHandler _addEditRemovePersonCommandHandler;

        private readonly Mock<IPersonRepository> _PersonRepositoryMock = new();
        private readonly Person expectedPerson;

        public Person_Add_Edit_Remove_CommandHandlerTest()
        {
            _addEditRemovePersonCommandHandler = new AddEditRemovePersonCommandHandler(_PersonRepositoryMock.Object);
            expectedPerson = new Person() 
            { 
                Id = Guid.NewGuid(), 
                FullName = "Full Name - Command Test - " + new Random().Next(1000).ToString()
            };
        }

        [Theory, MemberData(nameof(PersonData))]
        [Trait("AddEditRemovePersonCommand", "Positive")]
        public async Task Add_Edit_Remove_PersonCommand_Success(Guid personId, bool isDeleted)
        {
            // Arrange
            expectedPerson.Id = personId;
            var expectedPersonAddResponse = new ServiceResponse<Person>(HttpStatusCode.OK, new Person());
            _PersonRepositoryMock.Setup(PersonRepo => 
                PersonRepo.PersonAddEditRemove(It.IsAny<Person>(), isDeleted)).ReturnsAsync(expectedPersonAddResponse);

            // Act
            var actualAddEditRemovePersonResponse =
                await _addEditRemovePersonCommandHandler.Handle(
                    new AddEditRemovePersonCommand(expectedPerson, isDeleted), default);

            // Assert
            actualAddEditRemovePersonResponse.StatusCode.Should().Be(HttpStatusCode.OK);
            actualAddEditRemovePersonResponse.Errors.Should().BeEmpty();
        }

        [Theory, MemberData(nameof(PersonData))]
        [Trait("AddEditRemovePersonCommand", "Negative")]
        public async Task Add_Edit_Remove_PersonCommand_Error(Guid personId, bool isDeleted)
        {
            // Arrange
            expectedPerson.Id = personId;
            _PersonRepositoryMock.Setup(PersonRepo =>
                PersonRepo.PersonAddEditRemove(It.IsAny<Person>(), isDeleted)).ThrowsAsync(new Exception());

            // Act
            var actualAddEditRemovePersonResponse =
                await _addEditRemovePersonCommandHandler.Handle(
                    new AddEditRemovePersonCommand(expectedPerson, isDeleted), default);

            // Assert
            actualAddEditRemovePersonResponse.StatusCode.Should().Be(HttpStatusCode.InternalServerError);
            actualAddEditRemovePersonResponse.Errors.Should().Contain("Unexpected Error Has Occured");
        }

        #region PersonData

        // Person Data
        public static IEnumerable<object[]> PersonData
        {
            get
            {   
                //// PersonId , IsDeleted
                // Add
                yield return new object[] { Guid.Empty, false };
                // Edit
                yield return new object[] { Guid.NewGuid(), false };
                // Remove
                yield return new object[] { Guid.NewGuid(), true };
            }
        }

        #endregion PersonData
    }

}

