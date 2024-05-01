using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using LibraryManager.Application.Queries.GetLoanById;
using LibraryManager.Application.Queries.GetLoansAll;
using LibraryManager.Application.Commands.CreateLoan;
using LibraryManager.Application.Commands.UpdateLoan;
using LibraryManager.Application.Queries.GetLoansByBookId;
using LibraryManager.Application.Queries.GetLoansByUserId;

namespace LibraryManager.API.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/loans")]
    public class LoanController : ControllerBase
    {
        private readonly IMediator _mediator;
        public LoanController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var result = await _mediator.Send(new GetLoansAllQuery());

            return Ok(result);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            var result = await _mediator.Send(new GetLoanByIdQuery(id));

            return result.Success ? Ok(result) : NotFound(result.Message);
        }

        [HttpGet("book/{bookId}")]
        public async Task<IActionResult> GetLoansByBookId(Guid bookId)
        {
            var result = await _mediator.Send(new GetLoansByBookIdQuery(bookId));

            return result.Success ? Ok(result) : NotFound(result.Message);
        }

        [HttpGet("user/{userId}")]
        public async Task<IActionResult> GetLoansByUserId(Guid userId)
        {
            var result = await _mediator.Send(new GetLoansByUserIdQuery(userId));

            return result.Success ? Ok(result) : NotFound(result.Message);
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateLoanCommand command)
        {
            var result = await _mediator.Send(command);

            return result.Success ? Ok(result) : BadRequest(result.Message);
        }

        [HttpPut("devolution/{id}")]
        public async Task<IActionResult> Update(Guid id, UpdateLoanCommand command)
        {
            command.Id = id;
            var result = await _mediator.Send(command);

            return result.Success ? Ok(result) : BadRequest(result.Message);
        }
    }
}
