using Application.Command.Company;
using Application.Query.Company;
using MediatR;
using System.Reflection;

namespace API.Extensions
{
    public static class ServiceExtension
    {
        //public static IServiceCollection AddApplication(this IServiceCollection services)
        //{
        //    return services.AddMediatR(
        //        Assembly.GetExecutingAssembly(),
        //        typeof(GetAll).Assembly,
        //        typeof(AddCompanyCommand).Assembly);
        //}

    }
}
