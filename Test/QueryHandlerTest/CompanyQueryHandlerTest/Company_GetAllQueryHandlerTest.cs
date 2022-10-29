using Application.Query.Company;
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

namespace PhoneBook.UnitTest.QueryHandlerTest.CompanyQueryHandlerTest
{
    [Collection("GetAllCompanyQueryHandler")]
    public class Company_GetAllQueryHandlerTest
    {
        private readonly GetAllCompanyQueryHandler _getAllCompanyQueryHandler;
        private readonly Mock<ICompanyRepository> _companyRepositoryMock = new();

        public Company_GetAllQueryHandlerTest()
        {
            _getAllCompanyQueryHandler = new GetAllCompanyQueryHandler(_companyRepositoryMock.Object);
        }

        [Fact]
        [Trait("GetAllCompanyQuery", "Positive")]
        public async Task GetAllCompanyQuery_Success()
        {
            // Arrange
            ServiceResponse<List<Company>> expectedCompanyGetAllResponse = 
                new(HttpStatusCode.OK, CompanyTestData.CompanyDefaultData());

            _companyRepositoryMock.Setup(companyRepo => 
            companyRepo.CompanyGetAll()).ReturnsAsync(expectedCompanyGetAllResponse);

            // Act
            var actualGetAllCompanyResponse = await _getAllCompanyQueryHandler.Handle(new GetAllCompanyQuery(), default);

            // Assert
            actualGetAllCompanyResponse.StatusCode.Should().Be(HttpStatusCode.OK);
            actualGetAllCompanyResponse.Errors.Should().BeEmpty();
            actualGetAllCompanyResponse.Data.Should().Contain(expectedCompanyGetAllResponse.Data);
        }
    }
}
