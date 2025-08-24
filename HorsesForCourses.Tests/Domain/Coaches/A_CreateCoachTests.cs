using HorsesForCourses.Core.Domain.Coaches;
using QuickPulse.Explains;

namespace HorsesForCourses.Tests.Domain.Coaches;

[DocFile]
public class A_CreateCoachTests
{
    [Fact]
    public void CreateCoach_WithValidData_ShouldSucceed()
    {
        var coach = new Coach("Mark", "mark@example.com");

        Assert.Equal("Mark", coach.Name);
        Assert.Equal("mark@example.com", coach.Email);
        Assert.Empty(coach.Skills);
    }

    [Fact]
    [DocContent("Coach name cannot be an empty string.")]
    [DocExample(typeof(A_CreateCoachTests), nameof(CreateCoach_WithEmptyName_ShouldThrow))]
    [CodeSnippet]
    public void CreateCoach_WithEmptyName_ShouldThrow()
    {
        Assert.Throws<CoachNameCanNotBeEmpty>(
            () => new Coach("", "mail@example.com"));
    }

    [Fact]
    [DocContent("Coach email cannot be an empty string.")]
    [DocExample(typeof(A_CreateCoachTests), nameof(CreateCoach_WithEmptyEmail_ShouldThrow))]
    [CodeSnippet]
    public void CreateCoach_WithEmptyEmail_ShouldThrow()
    {
        Assert.Throws<CoachEmailCanNotBeEmpty>(
            () => new Coach("Coachy", ""));
    }
}
