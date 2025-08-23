using HorsesForCourses.Core.Domain;

namespace HorsesForCourses.Tests.Core;

public class SkillTests
{
    [Fact]
    public void CreateSkill_Valid_ShouldSucceed()
    {
        var skill = Skill.From("DotNet");

        Assert.Equal("DotNet", skill.Value);
    }

    [Theory]
    [InlineData(null)]
    [InlineData("")]
    public void CreateTimeSlot_Empty_ShouldThrow(string? value)
    {
        Assert.Throws<ArgumentException>(() => Skill.From(value!));
    }
}
