using System.Security.Authentication;
using Base.Contracts.DAL;
using Base.Contracts.Domain;
using Microsoft.EntityFrameworkCore;

namespace Base.DAL.EF;

public class BaseEntityRepository<TEntity, TDbContext> : BaseEntityRepository<TEntity, Guid, TDbContext> 
    where TEntity : class, IDomainAppUser<Guid>, IDomainEntityId<Guid>
    where TDbContext : DbContext
{
    
    public BaseEntityRepository(TDbContext dbContext) : base(dbContext)
    {
    }
}

public class BaseEntityRepository<TEntity, TKey, TDbContext> : IEntityRepository<TEntity, TKey>
    where TEntity : class, IDomainAppUser<TKey>, IDomainEntityId<TKey>
    where TKey : IEquatable<TKey>
    where TDbContext: DbContext
{
    protected readonly TDbContext RepoDbContext;
    protected readonly DbSet<TEntity> RepoDbSet;
    
    public BaseEntityRepository(TDbContext dbContext)
    {
        RepoDbContext = dbContext;
        RepoDbSet = dbContext.Set<TEntity>();
    }

    protected virtual IQueryable<TEntity> CreateQuery(TKey? userId, bool noTracking = true)
    {
        var query = RepoDbSet.AsQueryable();

        if (userId != null && !userId.Equals(default) &&
            typeof(IDomainEntityId<TKey>).IsAssignableFrom(typeof(TEntity)))
        {
            //TODO: fix appuser
            query = query.Where(e => e.AppUserId.Equals(userId));
        }

        if (noTracking)
        {
            query = query.AsNoTracking();
        }

        return query;
    }

    public virtual TEntity Add(TEntity entity)
    {
        return RepoDbSet.Add(entity).Entity;
    }

    public virtual TEntity Update(TEntity entity)
    {
        return RepoDbSet.Update(entity).Entity;
    }

    public virtual TEntity Remove(TEntity entity, TKey? userId)
    {
        return RepoDbSet.Remove(entity).Entity;
    }

    public virtual TEntity Remove(TKey id, TKey? userId)
    {
        var entity = FirstOrDefault(id, userId);
        if (entity == null)
        {
            throw new NullReferenceException($"Entity {typeof(TEntity).Name} with id {id} not found!");
        }
        return Remove(entity, userId);
    }

    public virtual TEntity? FirstOrDefault(TKey id, TKey? userId, bool noTracking = true)
    {
        return CreateQuery(userId, noTracking).FirstOrDefault(a => a.Id.Equals(id));
    }

    public virtual IEnumerable<TEntity> GetAll(TKey? userId, bool noTracking = true)
    {
        return CreateQuery(userId, noTracking).ToList(); 
    }

    public virtual bool Exists(TKey id, TKey? userId)
    {
        return RepoDbSet.Any(a => a.Id.Equals(id));
    }

    public virtual async Task<TEntity?> FirstOrDefaultAsync(TKey id, TKey? userId, bool noTracking = true)
    {
        var query = CreateQuery(userId, noTracking);
        return await query.FirstOrDefaultAsync(e => e.Id.Equals(id));
    }

    public virtual async Task<IEnumerable<TEntity>> GetAllAsync(TKey? userId, bool noTracking = true)
    {
        var query = CreateQuery(userId, noTracking);
        var res = await query.ToListAsync();

        return res!;
    }

    public virtual async Task<bool> ExistsAsync(TKey id, TKey? userId)
    {
        if (userId == null || userId.Equals(default))
            return await RepoDbSet.AnyAsync(e => e.Id.Equals(id));
        
        if (!typeof(IDomainEntityId<TKey>).IsAssignableFrom(typeof(TEntity)))
            throw new AuthenticationException(
                $"Entity {typeof(TEntity).Name} does not implement required interface: {typeof(IDomainEntityId<TKey>).Name} for AppUserId check");
        return await RepoDbSet.AnyAsync(e =>
            e.Id.Equals(id) && ((IDomainEntityId<TKey>) e).Id.Equals(userId));
    }

    public virtual async Task<TEntity> RemoveAsync(TKey id, TKey? userId)
    {
        var entity = await FirstOrDefaultAsync(id, userId);
        if (entity == null)
            throw new NullReferenceException($"Entity {typeof(TEntity).Name} with id {id} not found.");
        return Remove(entity!, userId);
    }
}