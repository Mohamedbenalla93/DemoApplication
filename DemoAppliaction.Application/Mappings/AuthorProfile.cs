using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using DemoApplication.Core.Application.DTOs.Author;
using DemoApplication.Core.Domain.Entities;

namespace DemoAppliaction.Core.Application.Mappings
{
    public class AuthorProfile : Profile
    {
        public AuthorProfile()
        {
            CreateMap<Author, AuthorDtoKeyValue>();
            CreateMap<Author, AuthorDtoFromBookListingPage>();
        }
    }
}
