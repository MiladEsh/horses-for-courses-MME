namespace HorsesForCourses.Core.Abstractions;

public record Id<T>
{
    private Guid value;
    private Id(Guid value) { this.value = value; }
    public override string ToString() => $"{typeof(T).Name}: {value}";
    public static Id<T> New() => new Id<T>(Guid.NewGuid());
}
