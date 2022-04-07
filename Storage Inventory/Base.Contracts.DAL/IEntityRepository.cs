using Base.Contracts.Domain;

namespace Base.Contracts.DAL;

public interface IEntityRepository<TEntity> : IEntityRepository<TEntity, Guid> 
    where TEntity: class, IDomainAppUser, IDomainEntityId
{
    
}

public interface IEntityRepository<TEntity, TKey>
    where TEntity : class, IDomainAppUser<TKey>, IDomainEntityId<TKey>
    where TKey : IEquatable<TKey>
{
    TEntity Add(TEntity entity);
    TEntity Update(TEntity entity);
    TEntity Remove(TEntity entity, TKey? userId);
    TEntity Remove(TKey id, TKey? userId);
    TEntity? FirstOrDefault(TKey id, TKey? userId, bool noTracking = true);
    IEnumerable<TEntity> GetAll(TKey? userId, bool noTracking = true);
    bool Exists(TKey id, TKey? userId);
    
    Task<TEntity?> FirstOrDefaultAsync(TKey id, TKey? userId, bool noTracking = true);
    Task<IEnumerable<TEntity>> GetAllAsync(TKey? userId, bool noTracking = true);
    Task<bool> ExistsAsync(TKey id, TKey? userId);
    Task<TEntity> RemoveAsync(TKey id, TKey? userId);
}