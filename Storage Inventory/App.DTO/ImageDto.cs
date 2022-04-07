using System.ComponentModel.DataAnnotations;
using Base.Domain;

namespace App.DTO;

public class ImageDto : DomainEntityId<Guid>
{
    [MaxLength(50)]
    public string ImageName { get; set; } = default!;

    public byte[] Bytes { get; set; } = default!;

    public Guid AppUserId { get; set; }
}