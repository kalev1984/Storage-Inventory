using AutoMapper;

namespace App.DTO.Mappers;

public class ImageMapper : BaseMapper<ImageDto, Domain.Image>
{
    public ImageMapper(IMapper mapper) : base(mapper)
    {
    }
}