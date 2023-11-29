using AutoMapper;
using UserManagement.DTO;
using UserManagement.Models;

namespace UserManagement.Configurations;

public class MapperProfileConfiguration : Profile
{
    public MapperProfileConfiguration()
    {
        CreateMap<UserProfile, UserProfileModel>().ReverseMap();
        CreateMap<User, UserModel>().ReverseMap();
    }
}
