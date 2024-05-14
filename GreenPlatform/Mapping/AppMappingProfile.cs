using AutoMapper;
using Domain.Entities;
using GreenPlatform.Domain.Dtos;

namespace GreenPlatform.Mapping;

public class AppMappingProfile : Profile
{
    public AppMappingProfile()
    {
        CreateMap<GreenPlatformUser, UserDto>().ReverseMap();
    }
}
