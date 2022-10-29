using MediatR;
using Persistence.Interface;
using Domain.Helper;
using Models = Domain.Models;
using System.Net;

namespace Application.Query.Person
{
    public record WildCardPersonQuery : IRequest<ServiceResponse<Models.Person>>;

    public class WildCardPersonQueryHandler : IRequestHandler<WildCardPersonQuery, ServiceResponse<Models.Person>>
    {
        private readonly IPersonRepository _personRepository;

        public WildCardPersonQueryHandler(IPersonRepository personRepository)
        {
            _personRepository = personRepository;
        }

        public async Task<ServiceResponse<Models.Person>> Handle(WildCardPersonQuery request, CancellationToken cancellationToken)
        {
            try
            {
                return await _personRepository.PersonWildCard();
            }
            catch (Exception ex)
            {
                return new ServiceResponse<Models.Person>(
                    HttpStatusCode.InternalServerError,
                    new Models.Person(),
                    new List<string>() { "Unexpected Error Has Occured", ex.Message });
            }
        }
    }
    
}
