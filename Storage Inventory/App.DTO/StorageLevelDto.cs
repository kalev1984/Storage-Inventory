using System.ComponentModel.DataAnnotations;
using Base.Domain;

namespace App.DTO;

public class StorageLevelDto : DomainEntityId<Guid>
{
    [MaxLength(25)]
    public string LevelName { get; set; } = default!;

    public Guid? ParentStorageLevelId { get; set; }

    public Guid? ItemId { get; set; }

    public Guid AppUserId { get; set; } 
}