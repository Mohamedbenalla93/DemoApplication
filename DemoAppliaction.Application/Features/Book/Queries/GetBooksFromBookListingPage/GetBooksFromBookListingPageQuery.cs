using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DemoAppliaction.Core.Application.Interfaces.Repositories;
using DemoAppliaction.Core.Application.Wrappers;
using MediatR;

namespace DemoApplication.Core.Application.Features.Book.Queries.GetBooksFromBookListingPage
{
    public class GetBooksFromBookListingPageQuery : IRequest<GetBooksFromBookListingPageViewModel>
    {
        public string? Title { get; set; }
        public DateTime? PusblishedAtFrom { get; set; }
        public DateTime? PublishedAtTo { get; set; }
        public string? AuthorFirstname { get; set; }
        public string? AuthorLastname { get; set; }
        public int PageNumber { get; set; }
        public int PageSize { get; set; }
    }

    public class GetBooksFromBookListingPageQueryHandler : IRequestHandler<GetBooksFromBookListingPageQuery, GetBooksFromBookListingPageViewModel>
    {
        private readonly IBookRepositoryAsync _bookRepositoryAsync;

        public GetBooksFromBookListingPageQueryHandler(IBookRepositoryAsync bookRepositoryAsync)
        {
            _bookRepositoryAsync = bookRepositoryAsync;
        }

        public async Task<GetBooksFromBookListingPageViewModel> Handle(GetBooksFromBookListingPageQuery request, CancellationToken cancellationToken)
        {
            if (request.PageSize <= 0)
                request.PageSize = 10;
            if (request.PageNumber <= 0)
                request.PageNumber = 1;

            var result = await _bookRepositoryAsync.GetBooksPagedResponseAsync(request.PageNumber, request.PageSize,
                request.Title, request.AuthorFirstname, request.AuthorLastname, request.PusblishedAtFrom,
                request.PublishedAtTo);

            var totalCount = await _bookRepositoryAsync.GetBooksCountAsync(request.Title, request.AuthorFirstname,
                request.AuthorLastname, request.PusblishedAtFrom,
                request.PublishedAtTo);

            return new GetBooksFromBookListingPageViewModel(result, request.PageNumber, request.PageSize, totalCount);

        }
    }
}
