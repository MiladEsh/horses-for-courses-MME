using HorsesForCourses.Core.Domain.Coaches.InvalidationReasons;
using HorsesForCourses.Core.Domain.Skills;


namespace HorsesForCourses.Tests.Documentation.Coaches.B_UpdateSkills;

public class B_UpdateSkillsDomain
{
    [Fact]
    public void CreateSkill_Valid_ShouldSucceed()
        => Assert.Equal("DotNet", Skill.From("DotNet").Value);

    [Theory]
    [InlineData(null)]
    [InlineData("")]
    public void Create_Skill_Empty_ShouldThrow(string? value)
        => Assert.Throws<SkillValueCanNotBeEmpty>(() => Skill.From(value!));

    [Fact]
    public void UpdateSkills_WithValidData_ShouldSucceed()
    {
        var coach = TheCannonical.Coach();
        var skills = new List<string> { "one", "two" };
        coach.UpdateSkills(skills);
        Assert.Equal([Skill.From("one"), Skill.From("two")], coach.Skills);
    }

    [Fact]
    public void UpdateSkills_WithInValidSkill_Throws()
    {
        var coach = TheCannonical.Coach();
        var skills = new List<string> { "", "two" };
        Assert.Throws<SkillValueCanNotBeEmpty>(() => coach.UpdateSkills(skills));
    }

    [Fact]
    public void UpdateSkills_With_Duplicates_Throws()
    {
        var coach = TheCannonical.Coach();
        var skills = new List<string> { "two", "two" };
        Assert.Throws<CoachAlreadyHasSkill>(() => coach.UpdateSkills(skills));
    }
}
