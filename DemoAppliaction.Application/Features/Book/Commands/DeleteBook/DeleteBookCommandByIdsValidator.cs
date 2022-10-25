using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DemoAppliaction.Core.Application.Interfaces.Repositories;
using FluentValidation;

namespace DemoApplication.Core.Application.Features.Book.Commands.DeleteBook
{
    public class DeleteBookCommandByIdsValidator : AbstractValidator<DeleteBookCommandByIds>
    {
        private readonly IBookRepositoryAsync _bookRepositoryAsync;

        public DeleteBookCommandByIdsValidator(IBookRepositoryAsync bookRepositoryAsync)
        {
            _bookRepositoryAsync = bookRepositoryAsync;

            RuleFor(x => x.ids)
                .Cascade(CascadeMode.Stop)
                .NotNull().WithMessage("{PropertyName} can't be null")
                .NotEmpty().WithMessage("{PropertyName} can't be empty")
                .MustAsync(IsAllBooksIdsValid).WithMessage("one or more books are not exist");
        }

        public async Task<bool> IsAllBooksIdsValid(IEnumerable<Guid> ids, CancellationToken cancellation) =>
            await _bookRepositoryAsync.CountAsync(x => ids.Contains(x.Id)) == ids.Count();
    }
}
