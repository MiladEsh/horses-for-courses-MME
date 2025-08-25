using HorsesForCourses.Core.Domain;
using HorsesForCourses.Core.Domain.Courses;

namespace HorsesForCourses.Tests.Core;

public class CourseTimeSlotRemovalTests
{
    [Fact]
    public void RemoveTimeSlot_BeforeConfirmation_ShouldRemove()
    {
        var course = new Course("RemoveTest", new DateOnly(2025, 9, 1), new DateOnly(2025, 9, 30));
        var slot = TimeSlot.From(CourseDay.Monday, OfficeHour.From(10), OfficeHour.From(12));
        course.AddTimeSlot(slot);

        course.RemoveTimeSlot(slot);

        Assert.Empty(course.TimeSlots);
    }

    [Fact]
    public void RemoveTimeSlot_AfterConfirmation_ShouldThrow()
    {
        var course = new Course("RemoveTest", new DateOnly(2025, 9, 1), new DateOnly(2025, 9, 30));
        var slot = TimeSlot.From(CourseDay.Tuesday, OfficeHour.From(13), OfficeHour.From(15));
        course.AddTimeSlot(slot);
        course.Confirm();

        Assert.Throws<InvalidOperationException>(() => course.RemoveTimeSlot(slot));
    }
}
