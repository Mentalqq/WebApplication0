using AutoMapper;
using WebApplication1.Domain;
using WebApplication1.DTO;
using WebApplication1.ViewModel;

namespace WebApplication1.Application.Options
{
    public class UserProfile : Profile
    {
        public UserProfile()
        {
            CreateMap<User, UserDto>();
            CreateMap<UserDto, User>();
            CreateMap<UserAddRequest, UserDto>();
            CreateMap<UserUpdateRequest, UserDto>();
        }
    }
}
