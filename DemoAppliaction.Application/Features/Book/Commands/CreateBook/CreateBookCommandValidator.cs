using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DemoAppliaction.Core.Application.Features.Book.Commands.CreateBook;
using DemoAppliaction.Core.Application.Interfaces.Repositories;
using FluentValidation;

namespace DemoApplication.Core.Application.Features.Book.Commands.CreateBook
{
    public class CreateBookCommandValidator : AbstractValidator<CreateBookCommand>
    {
        private readonly IAuthorRepositoryAsync _authorRepositoryAsync;
        private readonly IGenreRepositoryAsync _genreRepositoryAsync;

        public CreateBookCommandValidator(IAuthorRepositoryAsync authorRepositoryAsync, IGenreRepositoryAsync genreRepositoryAsync)
        {
            _authorRepositoryAsync = authorRepositoryAsync;
            _genreRepositoryAsync = genreRepositoryAsync;
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

        public async Task<bool> IsGenresValid(IEnumerable<Guid> ids, CancellationToken cancellationToken)
        {
            return await _genreRepositoryAsync.CountAsync(x => ids.Contains(x.Id)) == ids.Count();
        }
    }
}
