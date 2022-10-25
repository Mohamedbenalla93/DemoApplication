using DemoApplication.Core.Application.DTOs.Author;
using DemoApplication.Core.Application.DTOs.BookGenres;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DemoApplication.Core.Application.DTOs.Book
{
    public class BookDtoFromEditBookPage
    {
        public Guid Id { get; set; }
        public string Summary { get; set; }
        public DateTime PublishedAt { get; set; }
        public string Title { get; set; }
        public int NumberOfPages { get; set; }
        public  ICollection<BookGenreDtoFromEditBookPage> genresIds { get; set; }
        public Guid AuthorId { get; set; }
    }
}
