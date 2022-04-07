using AutoMapper;

namespace App.DTO.MappingProfiles;

public class AutoMapperProfile : Profile
{
    public AutoMapperProfile()
    {
        CreateMap<V1.Identity.AppRoleDto, Domain.Identity.AppRole>().ReverseMap();
        CreateMap<V1.Identity.AppUserDto, Domain.Identity.AppUser>().ReverseMap();
        CreateMap<ImageDto, Domain.Image>().ReverseMap();
        CreateMap<ItemDto, Domain.Item>().ReverseMap();
        CreateMap<StorageLevelDto, Domain.StorageLevel>().ReverseMap();
    }
}