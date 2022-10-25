using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DemoAppliaction.Core.Application.Interfaces.Repositories;
using FluentValidation;

namespace DemoApplication.Core.Application.Features.Book.Commands.UpdateBook
{
    public class UpdateBookCommandValidator : AbstractValidator<UpdateBookCommand>
    {
        private readonly IAuthorRepositoryAsync _authorRepositoryAsync;
        private readonly IGenreRepositoryAsync _genreRepositoryAsync;
        private readonly IBookRepositoryAsync _bookRepositoryAsync;

        public UpdateBookCommandValidator(IAuthorRepositoryAsync authorRepositoryAsync, IGenreRepositoryAsync genreRepositoryAsync, IBookRepositoryAsync bookRepositoryAsync)
        {
            _authorRepositoryAsync = authorRepositoryAsync;
            _genreRepositoryAsync = genreRepositoryAsync;
            _bookRepositoryAsync = bookRepositoryAsync;

            RuleFor(x => x.Id)
                .Cascade(CascadeMode.Stop)
                .NotNull().WithMessage("{PropertyName} can't be null")
                .NotEmpty().WithMessage("{PropertyName} can't be empty")
                .MustAsync(IsBookIdValid).WithMessage("{PropertyName} does not exist");

            RuleFor(x => x.Title)
                .Cascade(CascadeMode.Stop)
                .NotNull().WithMessage("{PropertyName} can't be null")
                .NotEmpty().WithMessage("{PropertyName} can't be empty")
                .MaximumLength(50).WithMessage("{PropertyName} must not exceed 50 caracters");

            RuleFor(x => x.NumberOfPages)
                .Cascade(CascadeMode.Stop)
                .NotNull().WithMessage("{PropertyName} can't be null")
                .GreaterThan(20).WithMessage("{PropertyName} can't be less than 20 pages");

            RuleFor(x => x.PublishedAt)
                .Cascade(CascadeMode.Stop)
                .NotNull().WithMessage("{PropertyName} can't be null")
                .NotEmpty().WithMessage("{PropertyName} can't be empty");

            RuleFor(x => x.AuthorId)
                .Cascade(CascadeMode.Stop)
                .NotNull().WithMessage("{PropertyName} can't be null")
                .MustAsync(IsAuthorIdValid).WithMessage("{PropertyName} does not exist");

            RuleFor(x => x.GenresIds)
                .Cascade(CascadeMode.Stop)
                .NotNull().WithMessage("{PropertyName} can't be null")
                .NotEmpty().WithMessage("{PropertyName} can't be empty")
                .MustAsync(IsGenresValid).WithMessage("one or more genres are not exist");
        }

        public async Task<bool> IsAuthorIdValid(Guid id, CancellationToken cancellationToken) =>
            await _authorRepositoryAsync.AnyAsync(x => x.Id == id);
        public async Task<bool> IsBookIdValid(Guid id, CancellationToken cancellationToken) =>
            await _bookRepositoryAsync.AnyAsync(x => x.Id == id);
        public async Task<bool> IsGenresValid(IEnumerable<Guid> ids, CancellationToken cancellationToken)
        {
            return await _genreRepositoryAsync.CountAsync(x => ids.Contains(x.Id)) == ids.Count();
        }
    }
}
