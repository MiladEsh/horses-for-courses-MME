namespace HorsesForCourses.Core.Abstractions;

public abstract class DomainEntity<T>
{
    public Id<T> Id { get; }
    public DomainEntity(Id<T> id) { Id = id; }
    public override bool Equals(object? obj)
    {
        if (obj is not DomainEntity<T> other) return false;
        return Id == other.Id;
    }
    public override int GetHashCode() => Id.GetHashCode();
}
