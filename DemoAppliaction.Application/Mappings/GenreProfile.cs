using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using DemoApplication.Core.Application.DTOs.Genre;
using DemoApplication.Core.Domain.Entities;

namespace DemoApplication.Core.Application.Mappings
{
    public class GenreProfile : Profile
    {
        public GenreProfile()
        {
            CreateMap<Genre, GenreDtoFromBookListingPage>();
            CreateMap<Genre, GenreDtoAsKeyValue>();
        }
    }
}
