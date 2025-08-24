using HorsesForCourses.Core.Domain.Coaches;
using HorsesForCourses.Core.Domain.Coaches.InvalidationReasons;
using QuickPulse.Explains;

namespace HorsesForCourses.Tests.Domain.Coaches;

[DocFile]
public class B_Domain
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
    [DocExample(typeof(B_Domain), nameof(CreateCoach_WithEmptyName_ShouldThrow))]
    [CodeSnippet]
    public void CreateCoach_WithEmptyName_ShouldThrow()
    {
        Assert.Throws<CoachNameCanNotBeEmpty>(
            () => new Coach("", "mail@example.com"));
    }

    [Fact]
    [DocContent("Coach email cannot be an empty string.")]
    [DocExample(typeof(B_Domain), nameof(CreateCoach_WithEmptyEmail_ShouldThrow))]
    [CodeSnippet]
    public void CreateCoach_WithEmptyEmail_ShouldThrow()
    {
        Assert.Throws<CoachEmailCanNotBeEmpty>(
            () => new Coach("Coachy", ""));
    }
}
