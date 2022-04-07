using AutoMapper;

namespace App.DTO.Mappers;

public class StorageLevelMapper : BaseMapper<StorageLevelDto, App.Domain.StorageLevel>
{
    public StorageLevelMapper(IMapper mapper) : base(mapper)
    {
    }
}