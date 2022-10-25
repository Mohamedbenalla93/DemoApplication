using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using DemoAppliaction.Core.Application.Features.Book.Commands.CreateBook;
using DemoApplication.Core.Application.DTOs.Book;
using DemoApplication.Core.Application.Features.Book.Commands.UpdateBook;
using DemoApplication.Core.Domain.Entities;

namespace DemoApplication.Core.Application.Mappings
{
    public class BookProfile : Profile
    {
        public BookProfile()
        {
            CreateMap<CreateBookCommand, Book>()
                .ForMember(x => x.BookGenres, opt => opt.Ignore());
            CreateMap<UpdateBookCommand, Book>()
                .ForMember(x => x.BookGenres, opt => opt.Ignore());
            CreateMap<Book, BookDtoFromBookListingPage>();
            CreateMap<Book, BookDtoFromEditBookPage>()
                .ForMember(m=> m.genresIds, opt=> opt.MapFrom(x=> x.BookGenres));
        }
    }
}
