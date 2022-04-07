using App.Contracts.DAL;
using App.DAL.EF.Repositories;
using Base.DAL.EF;

namespace App.DAL.EF;

public class AppUnitOfWork : BaseUnitOfWork<AppDbContext>, IAppUnitOfWork
{
    public AppUnitOfWork(AppDbContext uowDbContext) : base(uowDbContext)
    {
    }
    
    public IImageRepository Images =>
        GetRepository(() => new ImageRepository(UowDbContext));
    public IItemRepository Items =>
        GetRepository(() => new ItemRepository(UowDbContext));
    public IStorageLevelReposiotory StorageLevels =>
        GetRepository(() => new StorageLevelRepository(UowDbContext));
}