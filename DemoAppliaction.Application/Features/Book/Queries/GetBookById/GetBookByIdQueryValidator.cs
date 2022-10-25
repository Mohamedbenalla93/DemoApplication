using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DemoAppliaction.Core.Application.Interfaces.Repositories;
using FluentValidation;

namespace DemoApplication.Core.Application.Features.Book.Queries.GetBookById
{
    public class GetBookByIdQueryValidator : AbstractValidator<GetBookByIdQuery>
    {
        private readonly IBookRepositoryAsync _bookRepositoryAsync;

        public GetBookByIdQueryValidator(IBookRepositoryAsync bookRepositoryAsync)
        {
            _bookRepositoryAsync = bookRepositoryAsync;

            RuleFor(x => x.Id)
                .NotNull().WithMessage("id can't be null")
                .NotEmpty().WithMessage("id can't be empty")
                .MustAsync(CheckIfIdExist).WithMessage("id does not exist");
        }

        public async Task<bool> CheckIfIdExist(Guid id, CancellationToken cancellationToken) =>
            await _bookRepositoryAsync.AnyAsync(x => x.Id == id);
    }
}
