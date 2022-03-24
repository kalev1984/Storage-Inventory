using System.ComponentModel.DataAnnotations;
using Base.Contracts.Domain;
using Base.Domain;

namespace App.Domain;

public class Image : DomainEntityId<Guid>
{
    [MaxLength(50)]
    public string ImageName { get; set; } = default!;

    public byte[] Bytes { get; set; } = default!;

    public ICollection<Item>? Items { get; set; }
}