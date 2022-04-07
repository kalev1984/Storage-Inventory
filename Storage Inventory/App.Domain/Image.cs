using System.ComponentModel.DataAnnotations;
using App.Domain.Identity;
using Base.Contracts.Domain;
using Base.Domain;

namespace App.Domain;

public class Image : DomainEntityIdAppUserId
{
    [MaxLength(50)]
    public string ImageName { get; set; } = default!;

    public byte[] Bytes { get; set; } = default!;

    public AppUser? AppUser { get; set; }

    public ICollection<Item>? Items { get; set; }
}