using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using LibraryManager.Application.Queries.GetBooksAll;
using LibraryManager.Application.Queries.GetBookById;
using LibraryManager.Application.Commands.CreateBook;
using LibraryManager.Application.Commands.DeleteBook;
using LibraryManager.Application.Commands.UpdateBook;
using LibraryManager.Application.Queries.GetBooksAvaiables;

namespace LibraryManager.API.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/books")]
    public class BookController : ControllerBase
    {
        private readonly IMediator _mediator;

        public BookController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var result = await _mediator.Send(new GetBooksAllQuery());

            return result.Success ? Ok(result) : NotFound(result.Message);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            var result = await _mediator.Send(new GetBookByIdQuery(id));

            return result.Success ? Ok(result) : NotFound(result.Message);
        }

        [HttpGet("availables")]
        public async Task<IActionResult> GetAvailableBooks()
        {
            var result = await _mediator.Send(new GetBooksAvailableQuery());

            return result.Success ? Ok(result) : NotFound(result.Message);
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateBookCommand command)
        {
            var result = await _mediator.Send(command);

            return result.Success ? Ok(result) : BadRequest(result.Message);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(Guid id, UpdateBookCommand command)
        {
            command.Id = id;
            var result = await _mediator.Send(command);

            return result.Success ? Ok(result) : BadRequest(result.Message);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var command = new DeleteBookCommand(id);
            var result = await _mediator.Send(command);

            return result.Success ? Ok(result) : BadRequest(result.Message);
        }
    }
}
