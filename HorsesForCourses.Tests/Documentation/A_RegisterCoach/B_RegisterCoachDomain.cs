using HorsesForCourses.Core.Domain.Coaches;
using HorsesForCourses.Core.Domain.Coaches.InvalidationReasons;
using QuickPulse.Explains;

namespace HorsesForCourses.Tests.Documentation.A_RegisterCoach;

[DocFile]
[DocFileHeader("Domain")]
public class B_RegisterCoachDomain
{
    [Fact]
    public void RegisterCoach_WithValidData_ShouldSucceed()
    {
        var coach = new Coach("Mark", "mark@example.com");

        Assert.Equal("Mark", coach.Name);
        Assert.Equal("mark@example.com", coach.Email);
        Assert.Empty(coach.Skills);
    }

    [Fact]
    [DocContent("Coach name cannot be an empty string.")]
    public void RegisterCoach_WithEmptyName_ShouldThrow()
        => Assert.Throws<CoachNameCanNotBeEmpty>(
            () => new Coach("", "mail@example.com"));

    [Fact]
    [DocContent("Coach email cannot be an empty string.")]
    public void RegisterCoach_WithEmptyEmail_ShouldThrow()
        => Assert.Throws<CoachEmailCanNotBeEmpty>(() => new Coach("Coachy", ""));
}
