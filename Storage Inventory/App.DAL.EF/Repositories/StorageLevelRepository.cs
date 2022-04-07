using App.Contracts.DAL;
using App.Domain;
using Base.DAL.EF;

namespace App.DAL.EF.Repositories;

public class StorageLevelRepository : BaseEntityRepository<StorageLevel, AppDbContext>, IStorageLevelReposiotory
{
    public StorageLevelRepository(AppDbContext dbContext) : base(dbContext)
    {
    }
}