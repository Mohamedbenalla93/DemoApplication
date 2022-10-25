using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DemoApplication.Core.Domain.Entities
{
    public class Author : BaseEntityWithId, ISoftDelete
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime? BirthDay { get; set; }
        public string Image { get; set; }
        public string CarrierDescription { get; set; }
        public virtual  ICollection<Book> Books { get; set; }
    }
}
