namespace Base.Contracts.Domain;

public interface IDomainAppUser : IDomainAppUser<Guid>
{
        
}

public interface IDomainAppUser<TKey>
    where TKey : IEquatable<TKey>
{
    public TKey AppUserId { get; set; }
}