using Application.Command.Person;
using Application.Query.Person;
using Domain.Models;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    public class PersonController : BaseAPIController
    {
        [HttpGet]
        public async Task<ActionResult<List<Person>>> GetAll()
        {
            return Ok(await Mediator.Send(new GetAllPersonQuery()));
        }

        [HttpPost]
        public async Task<IActionResult> Add([FromBody] Person person)
        {
            return Ok(await Mediator.Send(new AddPersonCommand(person)));
        }

        [HttpPost]
        public async Task<IActionResult> Edit([FromBody] Person person)
        {
            return Ok(await Mediator.Send(new EditPersonCommand(person)));
        }

        [HttpPost]
        public async Task<IActionResult> Delete([FromBody] Person person)
        {
            return Ok(await Mediator.Send(new RemovePersonCommand(person)));
        }

        [HttpGet]
        public async Task<ActionResult<List<Person>>> Search(string searchText)
        {
            return Ok(await Mediator.Send(new SearchPersonQuery(searchText)));
        }

        [HttpGet]
        public async Task<ActionResult<Person>> WildCard()
        {
            return Ok(await Mediator.Send(new WildCardPersonQuery()));
        }
    }
}
