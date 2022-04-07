using AutoMapper;

namespace App.DTO.Mappers;

public class AppRoleMapper : BaseMapper<V1.Identity.AppRoleDto, Domain.Identity.AppRole>
{
    public AppRoleMapper(IMapper mapper) : base(mapper)
    {
    }
}