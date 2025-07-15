using HorsesForCourses.Core.Abstractions;

namespace HorsesForCourses.Core.Domain;

public record OfficeHour : ComparableValue<OfficeHour, int>
{
    public int Value { get; }

    protected override int InnerValue => Value;

    private OfficeHour(int value)
    {
        Value = value;
    }

    public static OfficeHour From(int value)
    {
        if (value < 9 || value > 17)
            throw new ArgumentException("Office hour should be between 09:00 - 17:00 (inclusive).");
        return new OfficeHour(value);
    }

    public static int operator -(OfficeHour a, OfficeHour b)
        => a.Value - b.Value;
}
