using App.Domain;
using App.Domain.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace App.DAL.EF;

public class AppDbContext : IdentityDbContext<AppUser, AppRole, Guid>
{
    public DbSet<Image> Images { get; set; } = default!;
    public DbSet<Item> Items { get; set; } = default!;
    public DbSet<StorageLevel> StorageLevels { get; set; } = default!;

    public AppDbContext(DbContextOptions<AppDbContext> option) : base(option)
    {
    }
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
            
        modelBuilder.Entity<StorageLevel>().
            HasOne(e => e.ParentStorageLevel).
            WithMany().
            HasForeignKey(m => m.ParentStorageLevelId);
            
        foreach (var relationship in modelBuilder
                     .Model.GetEntityTypes()
                     .SelectMany(e => e.GetForeignKeys()))
        {
            relationship.DeleteBehavior = DeleteBehavior.Restrict;
        }
    }
}