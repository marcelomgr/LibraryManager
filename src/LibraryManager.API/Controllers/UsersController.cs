using MediatR;
using Microsoft.AspNetCore.Mvc;
using LibraryManager.Application.Commands.SignUpUser;
using LibraryManager.Application.Commands.UpdateUser;
using LibraryManager.Application.Queries.GetUserById;
using LibraryManager.Application.Queries.GetUsersAll;
using LibraryManager.Application.Queries.GetUserByCpf;

namespace LibraryManager.API.Controllers
{
    [ApiController]
    [Route("api/users")]
    public class UsersController : ControllerBase
    {
        private readonly IMediator _mediator;
        public UsersController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet("GetAll")]
        public async Task<IActionResult> GetAll()
        {
            var query = new GetAllUsersQuery();

            var result = await _mediator.Send(query);

            if (!result.Success && result.Data == null)
            {
                return NotFound();
            }

            return Ok(result);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            var query = new GetUserByIdQuery(id);

            var result = await _mediator.Send(query);

            if (!result.Success && result.Data == null) {
                return NotFound();
            }

            return Ok(result);
        }
        
        [HttpGet("GetByCpf/{cpf}")]
        public async Task<IActionResult> GetByCpf(string cpf)
        {
            var query = new GetUserByCpfQuery(cpf);

            var result = await _mediator.Send(query);

            if (!result.Success && result.Data == null) {
                return NotFound();
            }

            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> Post(SignUpUserCommand command)
        {
            var result = await _mediator.Send(command);

            if (!result.Success)
            {
                return BadRequest(result.Message);
            }

            return CreatedAtAction(nameof(GetById), new { id = result.Data }, command);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Put(Guid id, UpdateUserCommand command)
        {
            command.Id = id;

            var result = await _mediator.Send(command);

            return Ok(result);
        }
    }
}
