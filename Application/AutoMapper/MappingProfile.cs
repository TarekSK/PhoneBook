using AutoMapper;
using Domain.Models;

namespace Application.Core
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Company, Company>();

            CreateMap<Person, Person>();
        }
    }
}
