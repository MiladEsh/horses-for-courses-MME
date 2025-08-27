using HorsesForCourses.Core.Domain.Courses.OfficeHours;

namespace HorsesForCourses.Core.Domain.Courses.TimeSlots;

public record TimeSlot
{
    public CourseDay Day { get; }
    public OfficeHour Start { get; } = OfficeHour.Empty;
    public OfficeHour End { get; } = OfficeHour.Empty;

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

    public bool OverlapsWith(TimeSlot otherTimeSlot)
    {
        if (Day != otherTimeSlot.Day) return false;
        if (Start < otherTimeSlot.End && End > otherTimeSlot.Start) return true;
        return false;
    }
}
