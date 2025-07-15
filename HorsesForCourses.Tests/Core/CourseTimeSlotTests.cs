using HorsesForCourses.Core.Domain;

namespace HorsesForCourses.Tests.Core;

public class CourseTimeSlotTests
{
    [Fact]
    public void AddTimeSlot_ValidSlot_ShouldAddToList()
    {
        var course = new Course("TDD", new DateOnly(2025, 1, 1), new DateOnly(2025, 2, 1));
        var slot = TimeSlot.From(CourseDay.Monday, OfficeHour.From(9), OfficeHour.From(11));

        course.AddTimeSlot(slot);

        Assert.Single(course.TimeSlots);
        Assert.Equal(slot, course.TimeSlots.First());
    }

    [Fact]
    public void AddTimeSlot_Duplicate_ShouldThrow()
    {
        var course = new Course("TDD", new DateOnly(2025, 1, 1), new DateOnly(2025, 2, 1));
        var slot = TimeSlot.From(CourseDay.Tuesday, OfficeHour.From(10), OfficeHour.From(12));
        course.AddTimeSlot(slot);

        Assert.Throws<InvalidOperationException>(() => course.AddTimeSlot(slot));
    }

    [Fact]
    public void AddTimeSlot_WhenCourseIsConfirmed_ShouldThrow()
    {
        var course = new Course("TDD", new DateOnly(2025, 1, 1), new DateOnly(2025, 2, 1));
        var slot = TimeSlot.From(CourseDay.Wednesday, OfficeHour.From(13), OfficeHour.From(15));

        typeof(Course).GetProperty("IsConfirmed")!.SetValue(course, true);

        Assert.Throws<InvalidOperationException>(() => course.AddTimeSlot(slot));
    }
}
