using DemoAppliaction.Core.Application.Features.Book.Commands.CreateBook;
using DemoApplication.Core.Application.Features.Book.Commands.DeleteBook;
using DemoApplication.Core.Application.Features.Book.Commands.UpdateBook;
using DemoApplication.Core.Application.Features.Book.Queries.GetBookById;
using DemoApplication.Core.Application.Features.Book.Queries.GetBooksFromBookListingPage;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WebApplication1.Controllers.v1
{
    public class BookController : BaseApiController
    {

        [HttpGet("get-books")]
        public async Task<IActionResult> GetBooks([FromQuery] GetBooksFromBookListingPageQuery request) =>
            Ok(await Mediator.Send(request));

        [HttpPost]
        public async Task<IActionResult> Post(CreateBookCommand command) => Ok(await Mediator.Send(command));

        [HttpPut]
        public async Task<IActionResult> Put(UpdateBookCommand command) => Ok(await Mediator.Send(command));

        [HttpGet("get-book-by-id")]
        public async Task<IActionResult> GetBook([FromQuery] GetBookByIdQuery request) =>
            Ok(await Mediator.Send(request));

        [HttpDelete]
        public async Task<IActionResult> Delete(Guid[] ids) =>
            Ok(await Mediator.Send(new DeleteBookCommandByIds() { ids = ids }));
    }
}
