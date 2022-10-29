using Domain.Helper;
using MediatR;
using Persistence.Interface;
using System.Net;
using Models = Domain.Models;

namespace Application.Query.Person
{
    public record SearchPersonQuery(string searchText) : IRequest<ServiceResponse<List<Models.Person>>>;

    public class SearchPersonQueryHandler : IRequestHandler<SearchPersonQuery, ServiceResponse<List<Models.Person>>>
    {
        private readonly IPersonRepository _personRepository;

        public SearchPersonQueryHandler(IPersonRepository personRepository)
        {
            _personRepository = personRepository;
        }

        public async Task<ServiceResponse<List<Models.Person>>> Handle(SearchPersonQuery request, CancellationToken cancellationToken)
        {
            try
            {
                return await _personRepository.PersonSearch(request.searchText);
            }
            catch (Exception ex)
            {
                return new ServiceResponse<List<Models.Person>>(
                    HttpStatusCode.InternalServerError,
                    new List<Models.Person>(),
                    new List<string>() { "Unexpected Error Has Occured", ex.Message });
            }
        }
    }
}
