using HorsesForCourses.Core.Domain;

namespace HorsesForCourses.Tests.Core;

public class TimeSlotTests
{
    [Fact]
    public void CreateTimeSlot_ValidWeekdayWithinOfficeHours_ShouldSucceed()
    {
        var slot = TimeSlot.From(CourseDay.Monday, OfficeHour.From(9), OfficeHour.From(11));

        Assert.Equal(CourseDay.Monday, slot.Day);
        Assert.Equal(OfficeHour.From(9), slot.Start);
        Assert.Equal(OfficeHour.From(11), slot.End);
    }

    [Fact]
    public void CreateTimeSlot_LessThanOneHour_ShouldThrow()
    {
        Assert.Throws<ArgumentException>(() =>
            TimeSlot.From(CourseDay.Wednesday, OfficeHour.From(14), OfficeHour.From(14))
        );
    }

    [Fact]
    public void CreateTimeSlot_EndBeforeStart_ShouldThrow()
    {
        Assert.Throws<ArgumentException>(() =>
            TimeSlot.From(CourseDay.Thursday, OfficeHour.From(15), OfficeHour.From(14))
        );
    }
}
