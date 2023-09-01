using AutoMapper;
using Unconnectedwebapi.Models;

namespace Unconnectedwebapi
{
    public class AutoMapperProfile:Profile
    {
        public AutoMapperProfile() {
            CreateMap<user, usermodel>()
                .ForMember(dest=>dest.useremail,opt=>opt.MapFrom(src=>src.useremail))
                .ForMember(dest=>dest.username,opt=>opt.MapFrom(src=>src.username)).ReverseMap();
        
        }
    }
}
