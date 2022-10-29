using Domain.Helper;
using MediatR;
using Persistence.Interface;
using System.Net;
using Models = Domain.Models;

namespace Application.Command.Person
{
    public record EditPersonCommand(Models.Person Person) : IRequest<ServiceResponse>;

    public class EditPersonCommandHandler : IRequestHandler<EditPersonCommand, ServiceResponse>
    {
        private readonly IPersonRepository _personRepository;

        public EditPersonCommandHandler(IPersonRepository personRepository)
        {
            _personRepository = personRepository;
        }

        public async Task<ServiceResponse> Handle(EditPersonCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var result = await _personRepository.PersonAddEditRemove(person: request.Person, isDelete: false);
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
