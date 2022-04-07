using Base.Contracts.Domain;

namespace Base.Domain;

public abstract class DomainEntityIdAppUserId : DomainEntityIdAppUserId<Guid>, IDomainEntityId, IDomainAppUser
{
}

public abstract class DomainEntityIdAppUserId<TKey> : IDomainEntityId<TKey>, IDomainAppUser<TKey>
    where TKey : IEquatable<TKey>
{
    public TKey Id { get; set; } = default!;
    public TKey AppUserId { get; set; } = default!;
}