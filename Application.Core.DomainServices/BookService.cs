using AutoMapper;
using DemoAppliaction.Core.Application.Exceptions;
using DemoAppliaction.Core.Application.Features.Book.Commands.CreateBook;
using DemoAppliaction.Core.Application.Interfaces;
using DemoApplication.Core.Application.Features.Book.Commands.UpdateBook;
using DemoApplication.Core.Application.Interfaces.Services;
using DemoApplication.Core.Domain.Entities;

namespace Application.Core.DomainServices
{
    public class BookService : IBookService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public BookService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<Guid> CreateBookAsync(CreateBookCommand command)
        {
            // start transaction
            await _unitOfWork.CreateTransactionAsync();

            try
            {
                //map command to the book entity
                var book = _mapper.Map<Book>(command);

                //add book to database
                await _unitOfWork.BookRepository.AddAsync(book);
                await _unitOfWork.CompleteAsync();

                //creating the genres of the book
                var bookGenres =
                    command.GenresIds.Select(genreId => new BookGenres() { BookId = book.Id, GenreId = genreId });

                //updating the database
                await _unitOfWork.BookGenresRepository.AddRangeAsync(bookGenres);
                await _unitOfWork.CompleteAsync();

                // commiting the transaction 
                await _unitOfWork.CommitAsync();

                return book.Id;
            }
            catch (Exception ex)
            {
                //if any runtime exception fired, we rollback all the updated data.
                await _unitOfWork.RollbackAsync();
                throw new ApiException(ex.Message);
            }
        }

        public async Task<bool> UpdateBookAsync(UpdateBookCommand command)
        {
            await _unitOfWork.CreateTransactionAsync();

            try
            {
                var book = await _unitOfWork.BookRepository.FirstOrDefaultAsync(x => x.Id == command.Id);

                var bookToUpdate = _mapper.Map(command, book);

                await _unitOfWork.BookRepository.UpdateAsync(bookToUpdate);

                await UpdateBookGenres(command.GenresIds, bookToUpdate.Id);

                await _unitOfWork.CompleteAsync();

                await _unitOfWork.CommitAsync();
                return true;
            }
            catch (Exception ex)
            {
                await _unitOfWork.RollbackAsync();
                throw new ApiException(ex.Message);
            }

        }

        public async Task UpdateBookGenres(IEnumerable<Guid> genresIds, Guid bookId)
        {

            foreach (var id in genresIds)
            {
                var bookGenre =
                    await _unitOfWork.BookGenresRepository.FirstOrDefaultWithoutQFAsync(x =>
                        x.BookId == bookId && x.GenreId == id);
                if (bookGenre == null)
                {
                    bookGenre = new BookGenres() { BookId = bookId, GenreId = id };
                    await _unitOfWork.BookGenresRepository.AddAsync(bookGenre);
                }
                else
                {
                    bookGenre.IsDeleted = false;
                    await _unitOfWork.BookGenresRepository.UpdateAsync(bookGenre);
                }
            }

            var bookGenresToDelete =
                await _unitOfWork.BookGenresRepository
                    .GetByConditionAsync(x =>
                        x.BookId == bookId &&
                        !genresIds.Contains(x.GenreId)
                    );
            if (bookGenresToDelete.Count > 0)
                await _unitOfWork.BookGenresRepository.DeleteRangeAsync(bookGenresToDelete);
        }

        public async Task<int> DeleteBookByIdsAsync(IEnumerable<Guid> ids)
        {
            var books = await _unitOfWork.BookRepository.GetByConditionAsync(x => ids.Contains(x.Id));

            await _unitOfWork.BookRepository.DeleteRangeAsync(books);
            return await _unitOfWork.CompleteAsync();
        }
    }
}