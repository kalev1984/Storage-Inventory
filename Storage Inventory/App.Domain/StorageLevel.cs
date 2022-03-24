using System.ComponentModel.DataAnnotations;
using Base.Domain;

namespace App.Domain;

public class StorageLevel : DomainEntityId<Guid>
{
    [MaxLength(25)]
    public string LevelName { get; set; } = default!;

    public Guid? ParentStorageLevelId { get; set; }
    public StorageLevel? ParentStorageLevel { get; set; }

    public Guid? ItemId { get; set; }
    public Item? Item { get; set; }
}