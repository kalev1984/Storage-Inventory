using System.ComponentModel.DataAnnotations;
using App.Domain.Identity;
using Base.Domain;

namespace App.Domain;

public class StorageLevel : DomainEntityIdAppUserId
{
    [MaxLength(25)]
    public string LevelName { get; set; } = default!;

    public Guid? ParentStorageLevelId { get; set; }
    public StorageLevel? ParentStorageLevel { get; set; }

    public Guid? ItemId { get; set; }
    public Item? Item { get; set; }

    public AppUser? AppUser { get; set; }  
}