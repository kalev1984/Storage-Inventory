using System.ComponentModel.DataAnnotations;
using App.Domain.Identity;
using Base.Domain;

namespace App.Domain;

public class Item : DomainEntityIdAppUserId
{
    [MaxLength(50)]
    public string Title { get; set; } = default!;

    [MaxLength(25)]
    public string? SerialNumber { get; set; }

    [MaxLength(25)]
    public string Color { get; set; } = default!;

    [Range(0, int.MaxValue)]
    public int Quantity { get; set; }

    [MaxLength(200)]
    public string Comment { get; set; } = default!;

    public Guid? ImageId { get; set; }
    public Image? Image { get; set; }

    public AppUser? AppUser { get; set; }

    public ICollection<StorageLevel>? StorageLevels { get; set; }
}