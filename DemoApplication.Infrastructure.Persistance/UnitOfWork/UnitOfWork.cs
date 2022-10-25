using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DemoAppliaction.Core.Application.Interfaces;
using DemoAppliaction.Core.Application.Interfaces.Repositories;
using DemoApplication.Infrastructure.Persistance.Context;
using Microsoft.EntityFrameworkCore.Storage;

namespace DemoApplication.Infrastructure.Persistance.UnitOfWork
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ApplicationDbContext _dbContext;
        public IDbContextTransaction Transaction;
        public IBookRepositoryAsync BookRepository { get; }
        public IAuthorRepositoryAsync AuthorRepository { get; }
        public IGenreRepositoryAsync GenreRepository { get; }
        public IBookGenresRepositoryAsync BookGenresRepository { get; }

        public UnitOfWork(ApplicationDbContext dbContext, IBookRepositoryAsync bookRepositoryAsync, IAuthorRepositoryAsync authorRepositoryAsync, IGenreRepositoryAsync genreRepositoryAsync, IBookGenresRepositoryAsync bookGenresRepositoryAsync)
        {
            _dbContext = dbContext;
            this.BookRepository = bookRepositoryAsync;
            AuthorRepository = authorRepositoryAsync;
            GenreRepository = genreRepositoryAsync;
            BookGenresRepository = bookGenresRepositoryAsync;
        }
        public async Task<int> CompleteAsync()
        {
            return await _dbContext.SaveChangesAsync();
        }

        public async Task DisposeAsync()
        {
            await _dbContext.DisposeAsync();
        }
        public async Task CreateTransactionAsync()
        {
            if (_dbContext.Database.CurrentTransaction != null)
                return;
            Transaction = await _dbContext.Database.BeginTransactionAsync();
        }
        public async Task CommitAsync()
        {
            await Transaction.CommitAsync();
        }
        public async Task RollbackAsync()
        {

            if (_dbContext.Database.CurrentTransaction is null)
            {
                return;
            }
            await Transaction.RollbackAsync();
        }
        public void Dispose()
        {
            DisposeAsync().Wait();
        }
    }
}
