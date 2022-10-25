using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DemoApplication.Core.Domain.Entities
{
    public class BookGenres : BaseEntityWithId, ISoftDelete
    {
        public Guid GenreId { get; set; }
        public Guid BookId { get; set; }

        public Book Book { get; set; }
        public Genre Genre { get; set; }
    }
}
