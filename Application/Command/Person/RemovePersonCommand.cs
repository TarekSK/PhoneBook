using Domain.Helper;
using MediatR;
using Persistence.Interface;
using System.Net;
using Models = Domain.Models;

namespace Application.Command.Person
{
    public record RemovePersonCommand(Models.Person Person) : IRequest<ServiceResponse>;

    public class RemovePersonCommandHandler : IRequestHandler<RemovePersonCommand, ServiceResponse>
    {
        private readonly IPersonRepository _personRepository;

        public RemovePersonCommandHandler(IPersonRepository personRepository)
        {
            _personRepository = personRepository;
        }

        public async Task<ServiceResponse> Handle(RemovePersonCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var result = await _personRepository.PersonAddEditRemove(person: request.Person, isDelete: true);
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
