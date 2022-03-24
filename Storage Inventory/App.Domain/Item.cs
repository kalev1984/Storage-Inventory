using System.ComponentModel.DataAnnotations;
using Base.Domain;

namespace App.Domain;

public class Item : DomainEntityId<Guid>
{
    [MaxLength(50)]
    public string Title { get; set; } = default!;

    [MaxLength(25)]
    public string? SerialNumber { get; set; }

    [MaxLength(25)]
    public string Color { get; set; } = default!;

    public int Quantity { get; set; }

    [MaxLength(200)]
    public string Comment { get; set; } = default!;

    public Guid? ImageId { get; set; }
    public Image? Image { get; set; }

    public ICollection<StorageLevel>? StorageLevels { get; set; }
}