using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using DemoAppliaction.Core.Application.Interfaces.Repositories;
using DemoAppliaction.Core.Application.Wrappers;
using DemoApplication.Core.Application.DTOs.Author;
using MediatR;

namespace DemoApplication.Core.Application.Features.Author.Queries.GetAuthorsAsKeyValue
{
    public class GetAuthorAsKeyValueQuery : IRequest<Response<IEnumerable<AuthorDtoKeyValue>>>
    {
    }

    public class GetAuthorAsKeyValueQueryHandler: IRequestHandler<GetAuthorAsKeyValueQuery, Response<IEnumerable<AuthorDtoKeyValue>>>
    {
        private readonly IAuthorRepositoryAsync _authorRepositoryAsync;
        private readonly IMapper _mapper;

        public GetAuthorAsKeyValueQueryHandler(IAuthorRepositoryAsync authorRepositoryAsync, IMapper mapper)
        {
            _authorRepositoryAsync = authorRepositoryAsync;
            _mapper = mapper;
        }

        public async Task<Response<IEnumerable<AuthorDtoKeyValue>>> Handle(GetAuthorAsKeyValueQuery request, CancellationToken cancellationToken)
        {
            var authors = await _authorRepositoryAsync.GetAllAsync();
            var mappedAuthors = _mapper.Map<IReadOnlyList<AuthorDtoKeyValue>>(authors);
            return new Response<IEnumerable<AuthorDtoKeyValue>>(mappedAuthors);
        }
    }
}
