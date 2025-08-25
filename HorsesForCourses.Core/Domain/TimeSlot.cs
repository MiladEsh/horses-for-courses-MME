using HorsesForCourses.Core.Abstractions;
using HorsesForCourses.Core.Domain.Courses;

namespace HorsesForCourses.Core.Domain;

public record TimeSlot
{
    public Id<Course> CourseId { get; private set; }

    public CourseDay Day { get; }

    public int StartHour => Start.Value;
    public OfficeHour Start { get; }

    public int EndHour => Start.Value;
    public OfficeHour End { get; }

    private TimeSlot() { }

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
