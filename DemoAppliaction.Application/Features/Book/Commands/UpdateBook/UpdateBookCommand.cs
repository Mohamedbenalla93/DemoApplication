using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DemoAppliaction.Core.Application.Wrappers;
using DemoApplication.Core.Application.Interfaces.Services;
using MediatR;

namespace DemoApplication.Core.Application.Features.Book.Commands.UpdateBook
{
    public class UpdateBookCommand : IRequest<Response<bool>>
    {
        public Guid Id { get; set; }
        public string Summary { get; set; }
        public DateTime PublishedAt { get; set; }
        public string Title { get; set; }
        public int NumberOfPages { get; set; }
        public IEnumerable<Guid> GenresIds { get; set; }
        public Guid AuthorId { get; set; }
    }

    public class UpdateBookCommandHandler : IRequestHandler<UpdateBookCommand, Response<bool>>
    {
        private readonly IBookService _bookService;

        public UpdateBookCommandHandler(IBookService bookService)
        {
            _bookService = bookService;
        }

        public async Task<Response<bool>> Handle(UpdateBookCommand request, CancellationToken cancellationToken)
        {
            var result = await _bookService.UpdateBookAsync(request);
            return new Response<bool>(result);
        }
    }
}
