using HorsesForCourses.Core.Abstractions;
using HorsesForCourses.Core.Domain.Courses;
using HorsesForCourses.Core.Domain.Courses.InvalidationReasons;

namespace HorsesForCourses.Core.Domain;

public record TimeSlot
{
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
            throw new TimeSlotMustBeAtleastOneHourLong();
        return new TimeSlot(day, start, end);
    }
}
