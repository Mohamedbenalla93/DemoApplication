using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DemoApplication.Core.Application.DTOs.Author;
using DemoApplication.Core.Application.DTOs.BookGenres;

namespace DemoApplication.Core.Application.DTOs.Book
{
    public class BookDtoFromBookListingPage
    {
        public Guid Id { get; set; }
        public string Summary { get; set; }
        public DateTime PublishedAt { get; set; }
        public string Title { get; set; }
        public int NumberOfPages { get; set; }
        public virtual ICollection<BookGenreDtoFromBookListingPage> BookGenres { get; set; }
        public AuthorDtoFromBookListingPage Author { get; set; }
    }
}
