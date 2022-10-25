using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using DemoAppliaction.Core.Application.Interfaces.Repositories;
using DemoAppliaction.Core.Application.Wrappers;
using DemoApplication.Core.Application.DTOs.Book;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace DemoApplication.Core.Application.Features.Book.Queries.GetBookById
{
    public class GetBookByIdQuery : IRequest<Response<BookDtoFromEditBookPage>>
    {
        public Guid Id { get; set; }
    }

    public class GetBookByIdQueryHandler : IRequestHandler<GetBookByIdQuery, Response<BookDtoFromEditBookPage>>
    {
        private readonly IBookRepositoryAsync _bookRepositoryAsync;
        private readonly IMapper _mapper;

        public GetBookByIdQueryHandler(IBookRepositoryAsync bookRepositoryAsync, IMapper mapper)
        {
            _bookRepositoryAsync = bookRepositoryAsync;
            _mapper = mapper;
        }

        public async Task<Response<BookDtoFromEditBookPage>> Handle(GetBookByIdQuery request, CancellationToken cancellationToken)
        {
            var book = await _bookRepositoryAsync.FirstOrDefaultAsync(x => x.Id == request.Id,includes: books => books.Include(x=> x.BookGenres));
            var mappedBook = _mapper.Map<BookDtoFromEditBookPage>(book);
            return new Response<BookDtoFromEditBookPage>(mappedBook);
        }
    }
}
