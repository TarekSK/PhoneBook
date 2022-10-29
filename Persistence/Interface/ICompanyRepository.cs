using Domain.Helper;
using Domain.Models;

namespace Persistence.Interface
{
    public interface ICompanyRepository
    {
        Task<ServiceResponse<Company>> CompanyAdd(Company company);

        Task<ServiceResponse<List<Company>>> CompanyGetAll();
    }
}
