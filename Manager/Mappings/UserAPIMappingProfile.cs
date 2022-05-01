using AutoMapper;
using Core.Domain;
using Core.Shared.ModelViews.UserAPI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Manager.Mappings
{
    public class UserAPIMappingProfile : Profile
    {

        public UserAPIMappingProfile()
        {
            CreateMap<UserAPI, UserAPIView>().ReverseMap();
            CreateMap<UserAPI, NewUserAPI>().ReverseMap();
            CreateMap<UserAPI, UserAPILogged>().ReverseMap();
            CreateMap<UserProfile, UserProfileView>().ReverseMap();
            CreateMap<UserProfile, ReferenceUserProfile>().ReverseMap();

        }
    }
}
