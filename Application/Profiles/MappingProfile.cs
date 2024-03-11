using AutoMapper;
using SocialSync.Application.DTO.UserDTO.DTO;
using StyleHub.Application.DTO.UserDTO.DTO;
using SytleHub.Domain.Entities;

namespace StyleHub.Application.Profiles
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<UserResponseDTO, User>().ReverseMap();
            CreateMap<UserCreateDTO, User>().ReverseMap();
            CreateMap<UserUpdateDTO, User>().ReverseMap();
        }
    }
}