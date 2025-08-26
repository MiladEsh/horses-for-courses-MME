using HorsesForCourses.Core.Abstractions;
using HorsesForCourses.Core.Domain.Courses.InvalidationReasons;

namespace HorsesForCourses.Core.Domain;

public record OfficeHour : ComparableValue<OfficeHour, int>
{
    public int Value { get; }

    protected override int InnerValue => Value;

    private OfficeHour() { }

    private OfficeHour(int value)
    {
        Value = value;
    }

    public static OfficeHour Empty => new(-1); // Not sure about this

    public static OfficeHour From(int value)
    {
        if (value < 9 || value > 17)
            throw new InvalidOfficeHour();
        return new OfficeHour(value);
    }

    public static int operator -(OfficeHour a, OfficeHour b)
        => a.Value - b.Value;
}
