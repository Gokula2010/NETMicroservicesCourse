using AutoMapper;
using PlatformService.Dtos;
using PlatformService.Models;

namespace PlatformService.Profiles
{
    public class PlatformsProfile : Profile
    {
        public PlatformsProfile()
        {
            // Source -> Target
            CreateMap<PlatformModel, PlatformReadDto>();
            CreateMap<PlatformCreateDto, PlatformModel>();
        }
    }
}