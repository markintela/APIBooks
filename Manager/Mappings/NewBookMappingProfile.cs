using AutoMapper;
using Core.Domain;
using Core.Shared.ModelViews;
using Core.Shared.ModelViews.Book;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Manager.Mappings
{
    public class NewBookMappingProfile : Profile
    {

        public NewBookMappingProfile()
        {
            CreateMap<NewBook, Book>();
            CreateMap<AlterBook, Book>();
        }
      
    }
}
