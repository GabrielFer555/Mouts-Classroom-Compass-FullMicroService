using AutoMapper;
using MCC.Data.Dtos;
using MCC.Models;

namespace MCC.Profiles
{
    public class UserProfile : Profile
    {

        public UserProfile() {
            CreateMap<CreateUserDto, User>();
        }

    }
}
