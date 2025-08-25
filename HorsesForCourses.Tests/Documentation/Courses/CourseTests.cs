using HorsesForCourses.Core.Domain.Courses;
using HorsesForCourses.Core.Domain.Skills;

namespace HorsesForCourses.Tests.Core;

public class CourseTests
{

    [Fact]
    public void AddRequiredSkill_ShouldAddSkillIfNotPresent()
    {
        var course = new Course("Test", new DateOnly(2025, 1, 1), new DateOnly(2025, 1, 31));

        course.AddRequiredSkill(Skill.From("DotNet"));

        Assert.Contains(Skill.From("DotNet"), course.RequiredSkills);
    }

    [Fact]
    public void AddRequiredSkill_ShouldNotAddDuplicate()
    {
        var course = new Course("Test", new DateOnly(2025, 1, 1), new DateOnly(2025, 1, 31));

        course.AddRequiredSkill(Skill.From("Agile"));
        course.AddRequiredSkill(Skill.From("Agile"));

        Assert.Single(course.RequiredSkills);
    }

    [Fact]
    public void RemoveRequiredSkill_ShouldRemoveSkillIfPresent()
    {
        var course = new Course("Test", new DateOnly(2025, 1, 1), new DateOnly(2025, 1, 31));
        course.AddRequiredSkill(Skill.From("Backend"));

        course.RemoveRequiredSkill(Skill.From("Backend"));

        Assert.DoesNotContain(Skill.From("Backend"), course.RequiredSkills);
    }

    [Fact]
    public void AddOrRemoveSkill_ShouldThrow_WhenCourseIsConfirmed()
    {
        var course = new Course("Test", new DateOnly(2025, 1, 1), new DateOnly(2025, 1, 31));
        course.AddRequiredSkill(Skill.From("DevOps"));

        // Simuleer bevestiging
        typeof(Course).GetProperty("IsConfirmed")!.SetValue(course, true);

        Assert.Throws<InvalidOperationException>(() => course.AddRequiredSkill(Skill.From("Security")));
        Assert.Throws<InvalidOperationException>(() => course.RemoveRequiredSkill(Skill.From("DevOps")));
    }
}
