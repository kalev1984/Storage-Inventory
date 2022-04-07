using AutoMapper;

namespace App.DTO.Mappers;

public class AppUserMapper : BaseMapper<V1.Identity.AppUserDto, Domain.Identity.AppUser>
{
    public AppUserMapper(IMapper mapper) : base(mapper)
    {
    }
}