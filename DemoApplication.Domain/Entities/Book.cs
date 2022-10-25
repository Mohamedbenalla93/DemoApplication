using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DemoApplication.Core.Domain.Entities
{
    public class Book : BaseEntityWithId, ISoftDelete
    {
        public string Summary { get; set; }
        public DateTime PublishedAt { get; set; }
        public string Title { get; set; }
        public int NumberOfPages { get; set; }
        public virtual ICollection<BookGenres> BookGenres { get; set; }
        public Guid AuthorId { get; set; }
        public Author Author { get; set; }
    }
}
