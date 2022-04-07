using AutoMapper;

namespace App.DTO.Mappers;

public class ItemMapper : BaseMapper<ItemDto, Domain.Item>
{
    public ItemMapper(IMapper mapper) : base(mapper)
    {
    }
}