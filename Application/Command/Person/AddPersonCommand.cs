using Domain.Helper;
using MediatR;
using Persistence.Interface;
using System.Net;
using Models = Domain.Models;

namespace Application.Command.Person
{
    public record AddPersonCommand(Models.Person Person) : IRequest<ServiceResponse>;

    public class AddPersonCommandHandler : IRequestHandler<AddPersonCommand, ServiceResponse>
    {
        private readonly IPersonRepository _personRepository;

        public AddPersonCommandHandler(IPersonRepository personRepository)
        {
            _personRepository = personRepository;
        }

        public async Task<ServiceResponse> Handle(AddPersonCommand request, CancellationToken cancellationToken)
        {
            try
            {
                return await _personRepository.PersonAdd(request.Person);
            }
            catch (Exception ex)
            {
                return new ServiceResponse(
                    HttpStatusCode.InternalServerError, new List<string>() { "Unexpected Error Has Occured", ex.Message });
            }

        }
    }
    
}
