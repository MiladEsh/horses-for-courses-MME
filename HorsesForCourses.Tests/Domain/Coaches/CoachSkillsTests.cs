using HorsesForCourses.Core.Domain;
using HorsesForCourses.Core.Domain.Coaches;

namespace HorsesForCourses.Tests.Core;

public class CoachSkillsTests
{
    [Fact]
    public void AddSkill_ShouldAdd_WhenNotAlreadyPresent()
    {
        var coach = new Coach("Anna", "anna@example.com");
        coach.AddSkill(Skill.From("Agile"));
        Assert.Contains(Skill.From("Agile"), coach.Skills);
    }

    [Fact]
    public void AddSkill_ShouldThrowIfAddDuplicate()
    {
        var coach = new Coach("Sam", "sam@example.com");
        coach.AddSkill(Skill.From("Backend"));
        coach.AddSkill(Skill.From("Backend"));
        Assert.Single(coach.Skills);
    }

    [Fact]
    public void RemoveSkill_ShouldRemoveIfPresent()
    {
        var coach = new Coach("Lina", "lina@example.com");
        coach.AddSkill(Skill.From("DotNet"));
        coach.RemoveSkill(Skill.From("DotNet"));
        Assert.DoesNotContain(Skill.From("DotNet"), coach.Skills);
    }

    [Fact]
    public void IsSuitableFor_ShouldReturnTrue_WhenAllSkillsMatch()
    {
        var coach = new Coach("Anna", "anna@example.com");
        coach.AddSkill(Skill.From("Frontend"));
        coach.AddSkill(Skill.From("Backend"));

        var course = new Course("Fullstack", new DateOnly(2025, 1, 1), new DateOnly(2025, 1, 31));
        course.AddRequiredSkill(Skill.From("Frontend"));
        course.AddRequiredSkill(Skill.From("Backend"));

        Assert.True(coach.IsSuitableFor(course));
    }

    [Fact]
    public void IsSuitableFor_ShouldReturnFalse_WhenCoachLacksASkill()
    {
        var coach = new Coach("Tom", "tom@example.com");
        coach.AddSkill(Skill.From("Frontend"));

        var course = new Course("Fullstack", new DateOnly(2025, 1, 1), new DateOnly(2025, 1, 31));
        course.AddRequiredSkill(Skill.From("Frontend"));
        course.AddRequiredSkill(Skill.From("Backend"));

        Assert.False(coach.IsSuitableFor(course));
    }
}
