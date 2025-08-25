using HorsesForCourses.Core.Domain.Courses.InvalidationReasons;
using HorsesForCourses.Core.Domain.Skills;


namespace HorsesForCourses.Tests.Documentation.Courses.B_UpdateRequiredSkills;

public class B_UpdateRequiredSkillsDomain
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
    public void UpdateRequiredSkills_WithValidData_ShouldSucceed()
    {
        var course = TheCannonical.Course();
        var skills = new List<string> { "one", "two" };
        course.UpdateRequiredSkills(skills);
        Assert.Equal([Skill.From("one"), Skill.From("two")], course.RequiredSkills);
    }

    [Fact]
    public void UpdateRequiredSkills_WithInValidSkill_Throws()
    {
        var course = TheCannonical.Course();
        var skills = new List<string> { "", "two" };
        Assert.Throws<SkillValueCanNotBeEmpty>(() => course.UpdateRequiredSkills(skills));
    }

    [Fact]
    public void UpdateRequiredSkills_With_Duplicates_Throws()
    {
        var course = TheCannonical.Course();
        var skills = new List<string> { "two", "two" };
        Assert.Throws<CourseAlreadyHasSkill>(() => course.UpdateRequiredSkills(skills));
    }
}
