using AutoMapper;
using WebApplication1.Domain;
using WebApplication1.DTO;
using WebApplication1.ViewModel;

namespace WebApplication1.Application.Options
{
    public static class MapperConfig
    {
        public static IMapper MapperUserToUserDto()
        {
            var mapper = new MapperConfiguration(cfg => cfg.CreateMap<User, UserDto>()).CreateMapper();
            return mapper;
        }
        public static IMapper MapperUserDtoToUser()
        {
            var mapper = new MapperConfiguration(cfg => cfg.CreateMap<UserDto, User>()).CreateMapper();
            return mapper;
        }
        public static IMapper MapperUserAddRequestToUserDto()
        {
            var mapper = new MapperConfiguration(cfg => cfg.CreateMap<UserAddRequest, UserDto>()).CreateMapper();
            return mapper;
        }
        public static IMapper MapperUserUpdateRequestToUserDto()
        {
            var mapper = new MapperConfiguration(cfg => cfg.CreateMap<UserUpdateRequest, UserDto>()).CreateMapper();
            return mapper;
        }

    }
}
