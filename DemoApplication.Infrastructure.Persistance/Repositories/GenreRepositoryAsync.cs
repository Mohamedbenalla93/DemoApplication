using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DemoAppliaction.Core.Application.Interfaces.Repositories;
using DemoApplication.Core.Domain.Entities;
using DemoApplication.Infrastructure.Persistance.Context;
using DemoApplication.Infrastructure.Persistance.Repositories.GenericRepository;
using Microsoft.EntityFrameworkCore;

namespace DemoApplication.Infrastructure.Persistance.Repositories
{
    public class GenreRepositoryAsync : GenericRepositoryAsync<Genre>, IGenreRepositoryAsync
    {
        private readonly DbSet<Genre> _genres;
        public GenreRepositoryAsync(ApplicationDbContext Context) : base(Context)
        {
            _genres = Context.Set<Genre>();
        }
    }
}
