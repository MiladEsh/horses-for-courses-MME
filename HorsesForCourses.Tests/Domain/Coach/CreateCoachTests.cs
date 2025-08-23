using HorsesForCourses.Core.Domain.Coaches;

namespace HorsesForCourses.Tests.Core;

public class CreateCoachTests
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
    public void CreateCoach_WithEmptyName_ShouldThrow()
    {
        Assert.Throws<CoachNameCanNotBeEmpty>(() =>
            new Coach("", "mail@example.com"));
    }

    [Fact]
    public void CreateCoach_WithEmptyEmail_ShouldThrow()
    {
        Assert.Throws<CoachEmailCanNotBeEmpty>(() =>
            new Coach("Coachy", ""));
    }
}
