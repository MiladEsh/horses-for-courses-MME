using HorsesForCourses.Core.Domain;

namespace HorsesForCourses.Tests.Core;

public class CourseTests
{
    [Fact]
    public void CreateCourse_WithValidData_ShouldSucceed()
    {
        var course = new Course("C# Bootcamp", new DateOnly(2025, 7, 1), new DateOnly(2025, 7, 31));

        Assert.Equal("C# Bootcamp", course.Name);
        Assert.Equal(new DateOnly(2025, 7, 1), course.StartDate);
        Assert.Equal(new DateOnly(2025, 7, 31), course.EndDate);
    }

    [Fact]
    public void CreateCourse_WithEmptyName_ShouldThrow()
    {
        Assert.Throws<ArgumentException>(() =>
            new Course("", new DateOnly(2025, 7, 1), new DateOnly(2025, 7, 31))
        );
    }

    [Fact]
    public void CreateCourse_WithEndDateBeforeStartDate_ShouldThrow()
    {
        Assert.Throws<ArgumentException>(() =>
            new Course("Agile Training", new DateOnly(2025, 7, 31), new DateOnly(2025, 7, 1))
        );
    }

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
