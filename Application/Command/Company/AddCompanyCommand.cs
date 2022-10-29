using Domain.Helper;
using MediatR;
using Persistence.Interface;
using System.Net;
using Models = Domain.Models;

namespace Application.Command.Company
{
    public record AddCompanyCommand(Models.Company Company) : IRequest<ServiceResponse>;

    public class AddCompanyCommandHandler : IRequestHandler<AddCompanyCommand, ServiceResponse>
    {
        private readonly ICompanyRepository _companyRepository;

        public AddCompanyCommandHandler(ICompanyRepository companyRepository)
        {
            _companyRepository = companyRepository;
        }

        public async Task<ServiceResponse> Handle(AddCompanyCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var result = await _companyRepository.CompanyAdd(request.Company);
                return result;
            }
            catch (Exception ex)
            {
                return new ServiceResponse(
                    HttpStatusCode.InternalServerError, new List<string>() { "Unexpected Error Has Occured", ex.Message });
            }
        }
    }
    
}
