using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using DemoAppliaction.Core.Application.Interfaces.Repositories;
using DemoApplication.Core.Application.DTOs.Book;
using DemoApplication.Core.Domain.Entities;
using DemoApplication.Infrastructure.Persistance.Context;
using DemoApplication.Infrastructure.Persistance.Repositories.GenericRepository;
using Microsoft.EntityFrameworkCore;

namespace DemoApplication.Infrastructure.Persistance.Repositories
{
    public class BookRepositoryAsync : GenericRepositoryAsync<Book>, IBookRepositoryAsync
    {
        private readonly IMapper _mapper;
        private readonly DbSet<Book> _books;

        public BookRepositoryAsync(ApplicationDbContext dbContext, IMapper mapper) : base(dbContext)
        {
            _mapper = mapper;
            _books = dbContext.Set<Book>();
        }

        public async Task<IReadOnlyList<BookDtoFromBookListingPage>> GetBooksPagedResponseAsync(int pageNumber, int pageSize,
            string title, string authorFirstname, string authorLastname, DateTime? publishedAtFrom,
            DateTime? publishedAtTo)
        {
            var query = _books
                .Include(x => x.Author)
                .Include(x => x.BookGenres)
                .ThenInclude(x => x.Genre).AsNoTracking();

            if (!string.IsNullOrEmpty(title))
                query = query.Where(x => x.Title.Contains(title));

            if (!string.IsNullOrEmpty(authorFirstname))
                query = query.Where(x => x.Author.FirstName.Contains(authorFirstname));

            if (!string.IsNullOrEmpty(authorLastname))
                query = query.Where(x => x.Author.LastName.Contains(authorLastname));

            if (publishedAtFrom.HasValue)
                query = query.Where(x => x.PublishedAt.Date >= publishedAtFrom.Value.Date);

            if (publishedAtTo.HasValue)
                query = query.Where(x => x.PublishedAt.Date >= publishedAtTo.Value.Date);

            var result = await query
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ProjectTo<BookDtoFromBookListingPage>(_mapper.ConfigurationProvider)
                .ToListAsync();
            return result;
        }

        public async Task<int> GetBooksCountAsync(string title, string authorFirstname, string authorLastname, DateTime? publishedAtFrom, DateTime? publishedAtTo)
        {
            return await _books.AsNoTracking().CountAsync(book =>
                (!string.IsNullOrEmpty(title) ? book.Title.Contains(title) : 1 == 1) &&
                (!string.IsNullOrEmpty(authorFirstname) ? book.Author.FirstName.Contains(authorFirstname) : 1 == 1) &&
                (!string.IsNullOrEmpty(authorLastname) ? book.Author.LastName.Contains(authorLastname) : 1 == 1) &&
                (publishedAtFrom.HasValue ? book.PublishedAt.Date >= publishedAtFrom.Value.Date : 1 == 1) &&
                (publishedAtTo.HasValue ? book.PublishedAt.Date <= publishedAtTo.Value.Date : 1 == 1)
            );
        }
    }
}
