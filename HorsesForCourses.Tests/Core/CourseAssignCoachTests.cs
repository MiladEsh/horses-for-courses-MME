using HorsesForCourses.Core.Domain;

namespace HorsesForCourses.Tests.Core;

public class CourseAssignCoachTests
{
    private static Course CreateConfirmedCourseWithSlotAndSkill(Skill skill, TimeSlot slot)
    {
        var course = new Course("Testcursus", new DateOnly(2025, 9, 1), new DateOnly(2025, 9, 30));
        course.AddRequiredSkill(skill);
        course.AddTimeSlot(slot);
        course.Confirm();
        return course;
    }

    [Fact]
    public void AssignCoach_WithAllConditionsMet_ShouldAssignCourseToCoach()
    {
        var slot = TimeSlot.From(CourseDay.Tuesday, OfficeHour.From(10), OfficeHour.From(12));
        var course = CreateConfirmedCourseWithSlotAndSkill(Skill.From("Communication"), slot);

        var coach = new Coach("Emma", "emma@coach.com");
        coach.AddSkill(Skill.From("Communication"));

        course.AssignCoach(coach);

        Assert.Equal(coach, course.AssignedCoach);
        Assert.Single(coach.AssignedCourses);
        Assert.Contains(course, coach.AssignedCourses);
    }

    [Fact]
    public void AssignCoach_ShouldThrow_WhenCoachHasOverlappingSlotInOverlappingPeriod()
    {
        var conflictingCourse = CreateConfirmedCourseWithSlotAndSkill(
            Skill.From("DotNet"),
            TimeSlot.From(CourseDay.Wednesday, OfficeHour.From(14), OfficeHour.From(16))
        );

        var coach = new Coach("Max", "max@coach.com");
        coach.AddSkill(Skill.From("DotNet"));
        coach.AssignCourse(conflictingCourse);

        var newCourse = new Course("OverlapCursus", new DateOnly(2025, 9, 15), new DateOnly(2025, 9, 30));
        newCourse.AddRequiredSkill(Skill.From("DotNet"));
        newCourse.AddTimeSlot(TimeSlot.From(CourseDay.Wednesday, OfficeHour.From(15), OfficeHour.From(17)));
        newCourse.Confirm();

        Assert.Throws<InvalidOperationException>(() => newCourse.AssignCoach(coach));
    }
}
