using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using LibraryManager.Application.Commands.AuthUser;
using LibraryManager.Application.Commands.SignUpUser;
using LibraryManager.Application.Commands.UpdateUser;
using LibraryManager.Application.Queries.GetUserById;
using LibraryManager.Application.Queries.GetUsersAll;
using LibraryManager.Application.Queries.GetUserByCpf;

namespace LibraryManager.API.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/users")]
    public class UsersController : ControllerBase
    {
        private readonly IMediator _mediator;
        public UsersController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var query = new GetAllUsersQuery();

            var result = await _mediator.Send(query);

            if (!result.Success && result.Data is null)
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

            if (!result.Success && result.Data is null) {
                return NotFound();
            }

            return Ok(result);
        }
        
        [HttpGet("cpf={cpf}")]
        public async Task<IActionResult> GetByCpf(string cpf)
        {
            var query = new GetUserByCpfQuery(cpf);

            var result = await _mediator.Send(query);

            if (!result.Success && result.Data is null) {
                return NotFound();
            }

            return Ok(result);
        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> Post(SignUpUserCommand command)
        {
            var result = await _mediator.Send(command);

            if (!result.Success)
            {
                return BadRequest(result.Message);
            }

            return Ok(result);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Put(Guid id, UpdateUserCommand command)
        {
            command.Id = id;

            var result = await _mediator.Send(command);

            return Ok(result);
        }

        [HttpPost("auth")]
        [AllowAnonymous]
        public async Task<IActionResult> Auth([FromBody] AuthUserCommand command)
        {
            var result = await _mediator.Send(command);

            if (result is null)
            {
                return BadRequest();
            }

            return Ok(result);
        }
    }
}
