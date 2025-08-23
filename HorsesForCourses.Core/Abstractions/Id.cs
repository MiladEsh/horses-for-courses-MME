namespace HorsesForCourses.Core.Abstractions;

public record Id<T>
{
    private readonly int value;
    public int Value => value;

    public static Id<T> Empty { get; } = new Id<T>(default(int));

    private Id() { }

    private Id(int value) => this.value = value;

    //public override string ToString() => $"{typeof(T).Name}: {value}";

    //public static Id<T> New() => new Id<T>(Guid.NewGuid());

    public static Id<T> From(int value) => new(value);
}

