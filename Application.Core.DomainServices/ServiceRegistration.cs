using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DemoApplication.Core.Application.Interfaces.Services;
using Microsoft.Extensions.DependencyInjection;

namespace Application.Core.DomainServices
{
    public static class ServiceRegistration
    {
        public static void AddDomainServicesLayer(this IServiceCollection services)
        {
            services.AddScoped<IBookService, BookService>();
        }
    }
}
