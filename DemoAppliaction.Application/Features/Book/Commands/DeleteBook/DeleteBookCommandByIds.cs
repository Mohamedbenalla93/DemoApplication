using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DemoAppliaction.Core.Application.Wrappers;
using DemoApplication.Core.Application.Interfaces.Services;
using MediatR;

namespace DemoApplication.Core.Application.Features.Book.Commands.DeleteBook
{
    public class DeleteBookCommandByIds : IRequest<Response<int>>
    {
        public IEnumerable<Guid> ids { get; set; }
    }

    public class DeleteBookCommandByIdsHandler : IRequestHandler<DeleteBookCommandByIds, Response<int>>
    {
        private readonly IBookService _bookService;

        public DeleteBookCommandByIdsHandler(IBookService bookService)
        {
            _bookService = bookService;
        }

        public async Task<Response<int>> Handle(DeleteBookCommandByIds request, CancellationToken cancellationToken)
        {
            return new Response<int>(await _bookService.DeleteBookByIdsAsync(request.ids));
        }
    }
}
