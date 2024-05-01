using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using LibraryManager.Application.Commands.AuthUser;
using LibraryManager.Application.Commands.SignUpUser;
using LibraryManager.Application.Commands.UpdateUser;
using LibraryManager.Application.Queries.GetUserById;
using LibraryManager.Application.Queries.GetUsersAll;
using LibraryManager.Application.Commands.DeleteUser;
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

            return result.Success ? Ok(result) : NotFound(result.Message);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            var query = new GetUserByIdQuery(id);
            var result = await _mediator.Send(query);

            return result.Success ? Ok(result) : NotFound(result.Message);
        }

        [HttpGet("cpf={cpf}")]
        public async Task<IActionResult> GetByCpf(string cpf)
        {
            var query = new GetUserByCpfQuery(cpf);
            var result = await _mediator.Send(query);

            return result.Success ? Ok(result) : NotFound(result.Message);
        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> Post(CreateUserCommand command)
        {
            var result = await _mediator.Send(command);

            return result.Success ? Ok(result) : BadRequest(result.Message);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Put(Guid id, UpdateUserCommand command)
        {
            command.Id = id;
            var result = await _mediator.Send(command);

            return result.Success ? Ok(result) : BadRequest(result.Message);
        }

        [HttpPost("auth")]
        [AllowAnonymous]
        public async Task<IActionResult> Auth([FromBody] AuthUserCommand command)
        {
            var result = await _mediator.Send(command);

            return result.Success ? Ok(result) : BadRequest(result.Message);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var command = new DeleteUserCommand(id);
            var result = await _mediator.Send(command);

            return result.Success ? Ok(result) : BadRequest(result.Message);
        }
    }
}
