using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using DemoApplication.Core.Application.DTOs.BookGenres;
using DemoApplication.Core.Domain.Entities;

namespace DemoApplication.Core.Application.Mappings
{
    public class BookGenresProfile : Profile
    {
        public BookGenresProfile()
        {
            CreateMap<BookGenres, BookGenreDtoFromBookListingPage>();
            CreateMap<BookGenres, BookGenreDtoFromEditBookPage>();
        }
    }
}
