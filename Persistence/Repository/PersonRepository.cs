using AutoMapper;
using Domain.Helper;
using Domain.Models;
using Microsoft.EntityFrameworkCore;
using Persistence.Interface;
using System.Net;

namespace Persistence.Repository
{
    public class PersonRepository : IPersonRepository
    {
        private readonly DataContext _dataContext;
        private readonly IMapper _mapper;

        public PersonRepository(DataContext dataContext, IMapper mapper)
        {
            _dataContext = dataContext;
            _mapper = mapper;
        }

        public async Task<ServiceResponse<Person>> PersonAdd(Person person)
        {
            // Service Response - Init
            var serviceResponse = new ServiceResponse<Person>();

            try
            {
                // Save
                _dataContext.Persons.Add(person);
                await _dataContext.SaveChangesAsync();

                // Service Response - Created
                serviceResponse.Data = person;
                serviceResponse.StatusCode = HttpStatusCode.Created;
            }
            catch (Exception ex)
            {
                // Service Response - Error
                serviceResponse.StatusCode = HttpStatusCode.InternalServerError;
                serviceResponse.Errors.Add(ex.Message);
            }

            return serviceResponse;
        }

        public async Task<ServiceResponse<Person>> PersonAddEditRemove(Person person, bool isDelete)
        {
            // Service Response - Init
            var serviceResponse = new ServiceResponse<Person>();

            try
            {
                if (!isDelete)
                {
                    if(person.Id == Guid.Empty)
                    {
                        // Add
                        _dataContext.Persons.Add(person);
                        await _dataContext.SaveChangesAsync();

                        serviceResponse.Data = person;
                        serviceResponse.StatusCode = HttpStatusCode.OK;
                    }
                    else
                    {
                        // Edit
                        var personOld = PersonGet(person.Id).Result.Data;
                        _mapper.Map(person, personOld);
                        await _dataContext.SaveChangesAsync();

                        serviceResponse.StatusCode = HttpStatusCode.OK;
                    }
                }
                else
                {
                    // Remove - [In Case of Having Only Person Id]

                    // In This Case we have Person because it's a combined method
                    // _dataContext.Persons.Remove(person);

                    var personToDelete = PersonGet(person.Id).Result.Data;
                    _dataContext.Persons.Remove(personToDelete);
                    await _dataContext.SaveChangesAsync();

                    serviceResponse.StatusCode = HttpStatusCode.OK;
                }
            }
            catch (Exception ex)
            {
                // Service Response - Error
                serviceResponse.StatusCode = System.Net.HttpStatusCode.InternalServerError;
                serviceResponse.Errors.Add(ex.Message);
            }

            return serviceResponse;
        }

        public async Task<ServiceResponse<Person>> PersonGet(Guid personId)
        {
            // Service Response - Init
            var serviceResponse = new ServiceResponse<Person>();

            try
            {
                // Get Data
                var result = await _dataContext.Persons.FindAsync(personId);

                serviceResponse.Data = result;
                serviceResponse.StatusCode = HttpStatusCode.OK;
            }
            catch (Exception ex)
            {
                // Service Response - Error
                serviceResponse.StatusCode = HttpStatusCode.InternalServerError;
                serviceResponse.Errors.Add(ex.Message);
            }

            return serviceResponse;
        }

        public async Task<ServiceResponse<List<Person>>> PersonGetAll()
        {
            // Service Response - Init
            var serviceResponse = new ServiceResponse<List<Person>>();

            try
            {
                // Get Data
                var result = await _dataContext.Persons.ToListAsync();

                serviceResponse.Data = result;
                serviceResponse.StatusCode = HttpStatusCode.OK;
            }
            catch (Exception ex)
            {
                // Service Response - Error
                serviceResponse.StatusCode = HttpStatusCode.InternalServerError;
                serviceResponse.Errors.Add(ex.Message);
            }

            return serviceResponse;
        }

        public async Task<ServiceResponse<List<Person>>> PersonSearch(string searchText)
        {
            // Service Response - Init
            var serviceResponse = new ServiceResponse<List<Person>>();

            try
            {
                // Get Data
                var result = await _dataContext.Persons.Join(_dataContext.Companies,
                    p => p.CompanyId,
                    c => c.Id,
                    (p, c) => new { CompanyId = c.Id, CompanyName = c.Name, p.Id, p.FullName, p.PhoneNumber, p.Address })
                    .Where(
                        x => x.FullName.Contains(searchText) ||
                        x.PhoneNumber.Contains(searchText) ||
                        x.Address.Contains(searchText) ||
                        x.CompanyName.Contains(searchText))
                        .Select(p => new Person()
                        {
                            CompanyId = p.CompanyId,
                            Address = p.Address,
                            FullName = p.FullName,
                            PhoneNumber = p.PhoneNumber,
                            Id = p.Id
                        }).ToListAsync();

                serviceResponse.Data = result;
                serviceResponse.StatusCode = HttpStatusCode.OK;
            }
            catch (Exception ex)
            {
                // Service Response - Error
                serviceResponse.StatusCode = HttpStatusCode.InternalServerError;
                serviceResponse.Errors.Add(ex.Message);
            }

            return serviceResponse;
        }

        public async Task<ServiceResponse<Person>> PersonWildCard()
        {
            // Service Response - Init
            var serviceResponse = new ServiceResponse<Person>();

            try
            {
                // Get Data
                var result = await _dataContext.Persons.OrderBy(r => Guid.NewGuid()).Take(1).FirstAsync();

                serviceResponse.Data = result;
                serviceResponse.StatusCode = HttpStatusCode.OK;
            }
            catch (Exception ex)
            {
                // Service Response - Error
                serviceResponse.StatusCode = HttpStatusCode.InternalServerError;
                serviceResponse.Errors.Add(ex.Message);
            }

            return serviceResponse;
        }
    }
}
