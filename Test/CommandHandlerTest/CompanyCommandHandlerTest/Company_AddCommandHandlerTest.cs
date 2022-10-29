using Application.Command.Company;
using Domain.Helper;
using Domain.Models;
using FluentAssertions;
using Moq;
using Persistence.Interface;
using System;
using System.Net;
using System.Threading.Tasks;
using Xunit;

namespace PhoneBook.UnitTest.CommandHandlerTest.CompanyCommandHandlerTest
{
    [Collection("AddCompanyCommandHandler")]
    public class Company_AddCommandHandlerTest
    {
        private readonly AddCompanyCommandHandler _addCompanyCommandHandler;
        private readonly Mock<ICompanyRepository> _companyRepositoryMock = new();

        private readonly Company expectedCompany;

        public Company_AddCommandHandlerTest()
        {
            _addCompanyCommandHandler = new AddCompanyCommandHandler(_companyRepositoryMock.Object);
            expectedCompany = new Company() { Name = "Company Name - Add Command Test - " + new Random().Next(1000).ToString() };
        }

        [Fact]
        [Trait("AddCompanyCommand", "Positive")]
        public async Task AddCompanyCommand_Success()
        {
            // Arrange
            var expectedCompanyAddResponse = new ServiceResponse<Company>(HttpStatusCode.Created, new Company());
            _companyRepositoryMock.Setup(companyRepo => companyRepo.CompanyAdd(It.IsAny<Company>())).ReturnsAsync(expectedCompanyAddResponse);

            // Act
            var actualAddCompanyResponse = await _addCompanyCommandHandler.Handle(new AddCompanyCommand(expectedCompany), default);

            // Assert
            actualAddCompanyResponse.StatusCode.Should().Be(HttpStatusCode.Created);
            actualAddCompanyResponse.Errors.Should().BeEmpty();
        }

        [Fact]
        [Trait("AddCompanyCommand", "Negative")]
        public async Task AddCompanyCommand_Error()
        {
            // Arrange
            _companyRepositoryMock.Setup(companyRepo => companyRepo.CompanyAdd(It.IsAny<Company>())).ThrowsAsync(new Exception());

            // Act
            var actualAddCompanyResponse = await _addCompanyCommandHandler.Handle(new AddCompanyCommand(expectedCompany), default);

            // Assert
            actualAddCompanyResponse.StatusCode.Should().Be(HttpStatusCode.InternalServerError);
            actualAddCompanyResponse.Errors.Should().Contain("Unexpected Error Has Occured");
        }
    }
}
