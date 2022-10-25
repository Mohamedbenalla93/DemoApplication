using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DemoAppliaction.Core.Application.Wrappers;
using DemoApplication.Core.Application.DTOs.Book;

namespace DemoApplication.Core.Application.Features.Book.Queries.GetBooksFromBookListingPage
{
    public class GetBooksFromBookListingPageViewModel : PagedResponse<IReadOnlyList<BookDtoFromBookListingPage>>
    {
        public int TotalCount { get; set; }
        public GetBooksFromBookListingPageViewModel(IReadOnlyList<BookDtoFromBookListingPage> data, int pageNumber, int pageSize, int totalCount) : base(data, pageNumber, pageSize)
        {
            TotalCount = totalCount;
        }
    }
}
