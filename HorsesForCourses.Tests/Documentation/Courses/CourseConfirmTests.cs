using HorsesForCourses.Core.Domain;
using HorsesForCourses.Core.Domain.Courses;

namespace HorsesForCourses.Tests.Core;

public class CourseConfirmTests
{
    [Fact]
    public void ConfirmCourse_WithValidData_ShouldSetIsConfirmedTrue()
    {
        var course = new Course("Fullstack", new DateOnly(2025, 1, 1), new DateOnly(2025, 1, 31));
        course.AddTimeSlot(TimeSlot.From(CourseDay.Monday, OfficeHour.From(9), OfficeHour.From(12)));

        course.Confirm();

        Assert.True(course.IsConfirmed);
    }

    [Fact]
    public void ConfirmCourse_WithoutTimeSlots_ShouldThrow()
    {
        var course = new Course("No TimeSlots", new DateOnly(2025, 1, 1), new DateOnly(2025, 1, 31));

        Assert.Throws<InvalidOperationException>(() => course.Confirm());
    }

    [Fact]
    public void ConfirmCourse_Twice_ShouldThrow()
    {
        var course = new Course("Twice", new DateOnly(2025, 1, 1), new DateOnly(2025, 1, 31));
        course.AddTimeSlot(TimeSlot.From(CourseDay.Friday, OfficeHour.From(10), OfficeHour.From(12)));
        course.Confirm();

        Assert.Throws<InvalidOperationException>(() => course.Confirm());
    }
}
