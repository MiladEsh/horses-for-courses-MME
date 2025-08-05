namespace HorsesForCourses.Core.Abstractions;

public record Id<T>
{
    private Guid value;

    // Needed by EF Core
    public Guid Value => value;

    // EF needs a public parameterless constructor
    private Id() { }

    private Id(Guid value) => this.value = value;

    public override string ToString() => $"{typeof(T).Name}: {value}";

    public static Id<T> New() => new Id<T>(Guid.NewGuid());

    // Factory for EF Core when using ValueConverter
    public static Id<T> From(Guid guid) => new Id<T>(guid);
}

