using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using DemoAppliaction.Core.Application.Interfaces.Repositories;
using DemoAppliaction.Core.Application.Wrappers;
using DemoApplication.Core.Application.DTOs.Genre;
using MediatR;

namespace DemoApplication.Core.Application.Features.Genre.Queries
{
    public class GenreAsKeyValueQuery : IRequest<Response<IReadOnlyList<GenreDtoAsKeyValue>>>
    {
    }
    public class GenreAsKeyValueQueryHadnler : IRequestHandler<GenreAsKeyValueQuery, Response<IReadOnlyList<GenreDtoAsKeyValue>>>
    {
        private readonly IGenreRepositoryAsync _genreRepositoryAsync;
        private readonly IMapper _mapper;

        public GenreAsKeyValueQueryHadnler(IGenreRepositoryAsync genreRepositoryAsync, IMapper mapper)
        {
            _genreRepositoryAsync = genreRepositoryAsync;
            _mapper = mapper;
        }

        public async Task<Response<IReadOnlyList<GenreDtoAsKeyValue>>> Handle(GenreAsKeyValueQuery request, CancellationToken cancellationToken)
        {
            var _genres = await _genreRepositoryAsync.GetAllAsync();
            var mappedGenes = _mapper.Map<IReadOnlyList<GenreDtoAsKeyValue>>(_genres);

            return new Response<IReadOnlyList<GenreDtoAsKeyValue>>(mappedGenes);
        }
    }
}
