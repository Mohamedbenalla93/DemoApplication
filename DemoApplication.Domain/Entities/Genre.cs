using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DemoApplication.Core.Domain.Entities
{
    public class Genre : BaseEntityWithId, ISoftDelete
    {
        public string Name { get; set; }

        public virtual ICollection<BookGenres> BookGenres { get; set; }
    }
}
