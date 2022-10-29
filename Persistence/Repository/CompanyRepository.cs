using Domain.Helper;
using Domain.Models;
using Microsoft.EntityFrameworkCore;
using Persistence.Interface;
using System.Net;

namespace Persistence.Repository
{
    public class CompanyRepository : ICompanyRepository
    {
        private readonly DataContext _dataContext;

        public CompanyRepository(DataContext dataContext)
        {
            _dataContext = dataContext;
        }

        public async Task<ServiceResponse<Company>> CompanyAdd(Company company)
        {
            // Service Response - Init
            var serviceResponse = new ServiceResponse<Company>();

            try
            {
                // Save
                _dataContext.Companies.Add(company);
                await _dataContext.SaveChangesAsync();

                // Service Response - Created
                serviceResponse.Data = company;
                serviceResponse.StatusCode = HttpStatusCode.Created;
            }
            catch (Exception ex)
            {
                // Service Response - Error
                serviceResponse.StatusCode = HttpStatusCode.InternalServerError;
                serviceResponse.Errors.Add(ex.Message);
            }

            return serviceResponse;
        }

        public async Task<ServiceResponse<List<Company>>> CompanyGetAll()
        {
            // Service Response - Init
            var serviceResponse = new ServiceResponse<List<Company>>();

            try
            {
                // Get Data
                var result = await _dataContext.Companies.Include(x => x.Persons).ToListAsync();

                serviceResponse.Data = result;
                serviceResponse.StatusCode = HttpStatusCode.OK;
            }
            catch (Exception ex)
            {
                // Service Response - Error
                serviceResponse.StatusCode = HttpStatusCode.InternalServerError;
                serviceResponse.Errors.Add(ex.Message);
            }

            return serviceResponse;
        }
    }
}
