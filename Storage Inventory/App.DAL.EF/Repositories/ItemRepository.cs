using App.Contracts.DAL;
using App.Domain;
using Base.DAL.EF;

namespace App.DAL.EF.Repositories;

public class ItemRepository : BaseEntityRepository<Item, AppDbContext>, IItemRepository
{
    public ItemRepository(AppDbContext dbContext) : base(dbContext)
    {
    }
}