using AutoMapper;
using Core.Domain;
using Core.Shared.ModelViews;
using Core.Shared.ModelViews.Book;
using Core.Shared.ModelViews.Library;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Manager.Mappings
{
    public class NewLibraryMappingProfile : Profile
    {


        public NewLibraryMappingProfile()
        {
            CreateMap<NewLibrary, Library>();
            CreateMap<Library, LibraryView>();
            CreateMap<Book, ReferenceBook>().ReverseMap();
            CreateMap<Book, BookView>().ReverseMap();
            CreateMap<Book, NewBook>().ReverseMap();
            CreateMap<AlterLibrary, Library>().ReverseMap();
        }
     
    }
}
