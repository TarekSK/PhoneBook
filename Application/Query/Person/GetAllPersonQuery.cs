using Domain.Helper;
using MediatR;
using Persistence.Interface;
using System.Net;
using Models = Domain.Models;

namespace Application.Query.Person
{
    public record GetAllPersonQuery : IRequest<ServiceResponse<List<Models.Person>>>;

    public class GetAllPersonQueryHandler : IRequestHandler<GetAllPersonQuery, ServiceResponse<List<Models.Person>>>
    {
        private readonly IPersonRepository _personRepository;

        public GetAllPersonQueryHandler(IPersonRepository personRepository)
        {
            _personRepository = personRepository;
        }

        public async Task<ServiceResponse<List<Models.Person>>> Handle(GetAllPersonQuery request, CancellationToken cancellationToken)
        {
            try
            {
                return await _personRepository.PersonGetAll();
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
