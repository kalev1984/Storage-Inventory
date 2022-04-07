using Microsoft.EntityFrameworkCore;

namespace Base.DAL.EF;

public class BaseUnitOfWork<TDbContext> : BaseUnitOfWork
    where TDbContext : DbContext
{
    protected readonly TDbContext UowDbContext;
        
    public BaseUnitOfWork(TDbContext uowDbContext)
    {
        UowDbContext = uowDbContext;
    }
        
    public override Task<int> SaveChangesAsync()
    {
        return UowDbContext.SaveChangesAsync();
    }

    public override int SaveChanges()
    {
        return UowDbContext.SaveChanges();
    }
}