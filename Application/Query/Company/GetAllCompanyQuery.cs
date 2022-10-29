using Domain.Helper;
using MediatR;
using Persistence.Interface;
using System.Net;
using Models = Domain.Models;

namespace Application.Query.Company
{
    public record GetAllCompanyQuery : IRequest<ServiceResponse<List<Models.Company>>>;

    public class GetAllCompanyQueryHandler : IRequestHandler<GetAllCompanyQuery, ServiceResponse<List<Models.Company>>>
    {
        private readonly ICompanyRepository _companyRepository;

        public GetAllCompanyQueryHandler(ICompanyRepository companyRepository)
        {
            _companyRepository = companyRepository;
        }

        public async Task<ServiceResponse<List<Models.Company>>> Handle(GetAllCompanyQuery request, CancellationToken cancellationToken)
        {
            try
            {
                return await _companyRepository.CompanyGetAll();
            }
            catch (Exception ex)
            {
                return new ServiceResponse<List<Models.Company>>(
                    HttpStatusCode.InternalServerError,
                    new List<Models.Company>(),
                    new List<string>() { "Unexpected Error Has Occured", ex.Message });
            }
        }
    }
    
}
