using Base.Contracts.Domain;
using Microsoft.AspNetCore.Identity;

namespace App.Domain.Identity;

public class AppUser : IdentityUser<Guid>, IDomainEntityId
{
    public ICollection<Image>? Images { get; set; }

    public ICollection<Item>? Items { get; set; }

    public ICollection<StorageLevel>? StorageLevels { get; set; }
}