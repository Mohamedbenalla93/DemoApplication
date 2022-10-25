using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DemoAppliaction.Core.Application.Interfaces.Repositories;

namespace DemoAppliaction.Core.Application.Interfaces
{
    public interface IUnitOfWork
    {
        IBookRepositoryAsync BookRepository { get; }
        IAuthorRepositoryAsync AuthorRepository { get; }
        IGenreRepositoryAsync GenreRepository { get; }
        IBookGenresRepositoryAsync BookGenresRepository { get; }
        Task<int> CompleteAsync();
        Task CreateTransactionAsync();
        Task CommitAsync();
        Task RollbackAsync();
        Task DisposeAsync();
    }
}
