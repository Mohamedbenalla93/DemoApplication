using DemoAppliaction.Core.Application.Interfaces;
using DemoApplication.Infrastructure.Persistance.Context;
using DemoApplication.Infrastructure.Persistance.Repositories.GenericRepository;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DemoAppliaction.Core.Application.Interfaces.Repositories;
using DemoApplication.Infrastructure.Persistance.Repositories;
using Microsoft.EntityFrameworkCore;


namespace DemoApplication.Infrastructure.Persistance
{
    public static class ServiceRegistration
    {
        public static void AddPersistenceInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<ApplicationDbContext>((s, o) =>
            {
                var connectionString = configuration.GetConnectionString("DefaultConnection");

                o.UseSqlServer(connectionString,
                    b => b.MigrationsAssembly(typeof(ApplicationDbContext).Assembly.FullName));
            });

            #region Repositories

            services.AddScoped(typeof(IGenericRepositotyAsync<>), typeof(GenericRepositoryAsync<>));
            services.AddScoped<IBookRepositoryAsync, BookRepositoryAsync>();
            services.AddScoped<IAuthorRepositoryAsync, AuthorRepositoryAsync>();
            services.AddScoped<IBookGenresRepositoryAsync, BookGenresRepositoryAsync>();
            services.AddScoped<IGenreRepositoryAsync, GenreRepositoryAsync>();

            #endregion

            #region UoW

            services.AddScoped<IUnitOfWork, UnitOfWork.UnitOfWork>();

            #endregion
        }
    }
}
