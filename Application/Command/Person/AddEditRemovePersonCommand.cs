using Domain.Helper;
using MediatR;
using Persistence.Interface;
using System.Net;
using Models = Domain.Models;

namespace Application.Command.Person
{
    public record AddEditRemovePersonCommand(Models.Person Person, bool isDeleted) : IRequest<ServiceResponse>;

    public class AddEditRemovePersonCommandHandler : IRequestHandler<AddEditRemovePersonCommand, ServiceResponse>
    {
        private readonly IPersonRepository _personRepository;

        public AddEditRemovePersonCommandHandler(IPersonRepository personRepository)
        {
            _personRepository = personRepository;
        }

        public async Task<ServiceResponse> Handle(AddEditRemovePersonCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var result = await _personRepository.PersonAddEditRemove(request.Person, request.isDeleted);
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
