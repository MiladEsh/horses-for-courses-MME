using HorsesForCourses.Core.Domain;
using HorsesForCourses.Core.Domain.Coaches;
using HorsesForCourses.Core.Domain.Courses;

namespace HorsesForCourses.Tests.Core;

public class CoachAvailabilityTests
{
    private static Course CreateCourse(string name, DateOnly start, DateOnly end, params TimeSlot[] slots)
    {
        var course = new Course(name, start, end);
        foreach (var slot in slots)
            course.AddTimeSlot(slot);
        course.Confirm();
        return course;
    }

    [Fact]
    public void IsAvailableFor_ShouldReturnTrue_WhenNoOverlap()
    {
        var coach = new Coach("Ben", "ben@example.com");

        var existingCourse = CreateCourse("Reeds gepland",
            new DateOnly(2025, 8, 1), new DateOnly(2025, 8, 31),
            TimeSlot.From(CourseDay.Monday, OfficeHour.From(9), OfficeHour.From(10)));

        coach.AssignCourse(existingCourse);

        var newCourse = CreateCourse("Nieuwe cursus",
            new DateOnly(2025, 8, 1), new DateOnly(2025, 8, 31),
            TimeSlot.From(CourseDay.Monday, OfficeHour.From(10), OfficeHour.From(11)));

        Assert.True(coach.IsAvailableFor(newCourse));
    }

    [Fact]
    public void IsAvailableFor_ShouldReturnFalse_WhenOverlap()
    {
        var coach = new Coach("Ben", "ben@example.com");

        var existingCourse = CreateCourse("Reeds gepland",
            new DateOnly(2025, 8, 1), new DateOnly(2025, 8, 31),
            TimeSlot.From(CourseDay.Monday, OfficeHour.From(9), OfficeHour.From(11)));

        coach.AssignCourse(existingCourse);

        var overlappingCourse = CreateCourse("Overlappend",
            new DateOnly(2025, 8, 15), new DateOnly(2025, 8, 31),
            TimeSlot.From(CourseDay.Monday, OfficeHour.From(10), OfficeHour.From(12)));

        Assert.False(coach.IsAvailableFor(overlappingCourse));
    }

    [Fact]
    public void IsAvailableFor_ShouldReturnTrue_WhenDifferentDays()
    {
        var coach = new Coach("Ben", "ben@example.com");

        var existingCourse = CreateCourse("Maandagcursus",
            new DateOnly(2025, 8, 1), new DateOnly(2025, 8, 31),
            TimeSlot.From(CourseDay.Monday, OfficeHour.From(9), OfficeHour.From(11)));

        coach.AssignCourse(existingCourse);

        var tuesdayCourse = CreateCourse("Dinsdagcursus",
            new DateOnly(2025, 8, 1), new DateOnly(2025, 8, 31),
            TimeSlot.From(CourseDay.Tuesday, OfficeHour.From(9), OfficeHour.From(11)));

        Assert.True(coach.IsAvailableFor(tuesdayCourse));
    }

    [Fact]
    public void IsAvailableFor_ShouldReturnFalse_WhenExactSameSlotAndOverlap()
    {
        var coach = new Coach("Ben", "ben@example.com");

        var existingSlot = TimeSlot.From(CourseDay.Wednesday, OfficeHour.From(13), OfficeHour.From(15));

        var course1 = CreateCourse("Cursus A",
            new DateOnly(2025, 8, 1), new DateOnly(2025, 8, 31), existingSlot);

        var course2 = CreateCourse("Cursus B",
            new DateOnly(2025, 8, 10), new DateOnly(2025, 8, 20), existingSlot);

        coach.AssignCourse(course1);

        Assert.False(coach.IsAvailableFor(course2));
    }

    [Fact]
    public void IsAvailableFor_ShouldReturnTrue_WhenSameSlotButDifferentPeriod()
    {
        var coach = new Coach("Ben", "ben@example.com");

        var existingSlot = TimeSlot.From(CourseDay.Friday, OfficeHour.From(10), OfficeHour.From(12));

        var course1 = CreateCourse("Voorjaar",
            new DateOnly(2025, 1, 1), new DateOnly(2025, 3, 31), existingSlot);

        var course2 = CreateCourse("Najaar",
            new DateOnly(2025, 4, 1), new DateOnly(2025, 6, 30), existingSlot);

        coach.AssignCourse(course1);

        Assert.True(coach.IsAvailableFor(course2));
    }
}
