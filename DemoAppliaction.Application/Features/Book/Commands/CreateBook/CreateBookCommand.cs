using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DemoAppliaction.Core.Application.Wrappers;
using DemoApplication.Core.Application.Interfaces.Services;
using DemoApplication.Core.Domain.Entities;
using MediatR;

namespace DemoAppliaction.Core.Application.Features.Book.Commands.CreateBook
{

    public class CreateBookCommand : IRequest<Response<Guid>>
    {
        public string Summary { get; set; }
        public DateTime PublishedAt { get; set; }
        public string Title { get; set; }
        public int NumberOfPages { get; set; }
        public IEnumerable<Guid> GenresIds { get; set; }
        public Guid AuthorId { get; set; }
    }
    public class CreateBookCommandHandler : IRequestHandler<CreateBookCommand, Response<Guid>>
    {
        private readonly IBookService _bookService;

        public CreateBookCommandHandler(IBookService bookService)
        {
            _bookService = bookService;
        }

        public async Task<Response<Guid>> Handle(CreateBookCommand request, CancellationToken cancellationToken)
        {
            var bookId = await _bookService.CreateBookAsync(request);
            return new Response<Guid>(bookId);
        }
    }
}
