namespace HorsesForCourses.Core.Domain;

public record TimeSlot
{
    public CourseDay Day { get; }
    public OfficeHour Start { get; }
    public OfficeHour End { get; }

    private TimeSlot(CourseDay day, OfficeHour start, OfficeHour end)
    {
        Day = day;
        Start = start;
        End = end;
    }

    public static TimeSlot From(CourseDay day, OfficeHour start, OfficeHour end)
    {
        if (start >= end)
            throw new ArgumentException("Time slot must be at least 1 hour long.");

        return new TimeSlot(day, start, end);
    }
}
