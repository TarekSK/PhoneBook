using Domain.Helper;
using Domain.Models;

namespace Persistence.Interface
{
    public interface IPersonRepository
    {
        Task<ServiceResponse<Person>> PersonAdd(Person person);

        Task<ServiceResponse<Person>> PersonAddEditRemove(Person person, bool isDelete);

        Task<ServiceResponse<Person>> PersonGet(Guid personId);

        Task<ServiceResponse<List<Person>>> PersonGetAll();

        Task<ServiceResponse<List<Person>>> PersonSearch(string searchText);

        Task<ServiceResponse<Person>> PersonWildCard();
    }
}
