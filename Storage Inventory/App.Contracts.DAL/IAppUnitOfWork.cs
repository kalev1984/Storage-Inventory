using Base.Contracts.DAL;

namespace App.Contracts.DAL;

public interface IAppUnitOfWork : IUnitOfWork
{
    IImageRepository Images { get; }
    IItemRepository Items { get; }
    IStorageLevelReposiotory StorageLevels { get; }
}