using HorsesForCourses.Core.Domain;
using HorsesForCourses.Core.Domain.Coaches;
using HorsesForCourses.Core.Domain.Courses;
using HorsesForCourses.Core.Domain.Skills;

namespace HorsesForCourses.Tests.Core;

public class UnassignCoachTests
{
    private static Course SetupCourseWithCoach(out Coach coach, out TimeSlot slot)
    {
        var skill = Skill.From("Agile");
        coach = new Coach("Ben", "ben@coach.com");
        coach.AddSkill(skill);

        var course = new Course("Agile Course", new DateOnly(2025, 9, 1), new DateOnly(2025, 9, 30));
        slot = TimeSlot.From(CourseDay.Thursday, OfficeHour.From(10), OfficeHour.From(12));
        course.AddRequiredSkill(skill);
        course.AddTimeSlot(slot);
        course.Confirm();
        course.AssignCoach(coach);
        return course;
    }

    [Fact]
    public void UnassignCoach_ShouldRemoveCoachFromCourse_AndReleaseTimeSlots()
    {
        var course = SetupCourseWithCoach(out var coach, out var slot);

        course.UnassignCoach();

        Assert.Null(course.AssignedCoach);
        Assert.DoesNotContain(course, coach.AssignedCourses);
    }

    [Fact]
    public void UnassignCoach_WhenNoCoachAssigned_ShouldThrow()
    {
        var course = new Course("Unassigned Course", new DateOnly(2025, 10, 1), new DateOnly(2025, 10, 31));

        Assert.Throws<InvalidOperationException>(() => course.UnassignCoach());
    }
}
