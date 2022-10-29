using Application.Command.Company;
using Application.Query.Company;
using Domain.Models;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    public class CompanyController : BaseAPIController
    {
        [HttpGet]
        public async Task<ActionResult<List<Company>>> GetAll()
        {
            return Ok(await Mediator.Send(new GetAllCompanyQuery()));
        }

        [HttpPost]
        public async Task<ActionResult> Add([FromBody] Company company)
        {
           return Ok(await Mediator.Send(new AddCompanyCommand(company)));
        }
    }
}
