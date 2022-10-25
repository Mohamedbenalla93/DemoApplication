using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DemoApplication.Core.Application.DTOs.Book;
using DemoApplication.Core.Domain.Entities;

namespace DemoAppliaction.Core.Application.Interfaces.Repositories
{
    public interface IBookRepositoryAsync : IGenericRepositotyAsync<Book>
    {
        Task<IReadOnlyList<BookDtoFromBookListingPage>> GetBooksPagedResponseAsync(int pageNumber,int pageSize,string title, string authorFirstname,string authorLastname, DateTime? publishedAtFrom, DateTime? publishedAtTo);

        Task<int> GetBooksCountAsync(string title, string authorFirstname, string authorLastname,
            DateTime? publishedAtFrom, DateTime? publishedAtTo);
    }
}
