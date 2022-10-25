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
    public class AuthorRepositoryAsync : GenericRepositoryAsync<Author>, IAuthorRepositoryAsync
    {
        private readonly DbSet<Author> _authors;
        public AuthorRepositoryAsync(ApplicationDbContext Context) : base(Context)
        {
            _authors = Context.Set<Author>();
        }
    }
}
