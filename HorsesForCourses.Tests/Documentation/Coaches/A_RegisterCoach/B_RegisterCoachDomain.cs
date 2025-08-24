using HorsesForCourses.Core.Domain.Coaches;
using HorsesForCourses.Core.Domain.Coaches.InvalidationReasons;
using QuickPulse.Explains;

namespace HorsesForCourses.Tests.Documentation.Coaches.A_RegisterCoach;

[DocFile]
[DocFileHeader("Domain")]
public class B_RegisterCoachDomain
{
    [Fact]
    public void RegisterCoach_WithValidData_ShouldSucceed()
    {
        var coach = TheCannonical.Coach();
        Assert.Equal(TheCannonical.CoachName, coach.Name);
        Assert.Equal(TheCannonical.CoachEmail, coach.Email);
        Assert.Empty(coach.Skills);
    }

    [Fact]
    public void RegisterCoach_WithValidData_keeps_default_id()
    {
        var coach = TheCannonical.Coach();
        Assert.Equal(default, coach.Id.Value);
    }

    [Fact]
    [DocContent("Coach name cannot be empty.")]
    public void RegisterCoach_WithEmptyName_ShouldThrow()
        => Assert.Throws<CoachNameCanNotBeEmpty>(
            () => new Coach(string.Empty, TheCannonical.CoachEmail));

    [Fact]
    [DocContent("Coach email cannot be empty.")]
    public void RegisterCoach_WithEmptyEmail_ShouldThrow()
        => Assert.Throws<CoachEmailCanNotBeEmpty>(() => new Coach(TheCannonical.CoachName, string.Empty));
}
