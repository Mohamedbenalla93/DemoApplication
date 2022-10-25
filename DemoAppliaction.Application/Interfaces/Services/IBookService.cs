using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DemoAppliaction.Core.Application.Features.Book.Commands.CreateBook;
using DemoApplication.Core.Application.Features.Book.Commands.UpdateBook;

namespace DemoApplication.Core.Application.Interfaces.Services
{
    public interface IBookService
    {
        Task<Guid> CreateBookAsync(CreateBookCommand command);
        Task<bool> UpdateBookAsync(UpdateBookCommand command);
        Task<int> DeleteBookByIdsAsync(IEnumerable<Guid> ids);
    }
}
