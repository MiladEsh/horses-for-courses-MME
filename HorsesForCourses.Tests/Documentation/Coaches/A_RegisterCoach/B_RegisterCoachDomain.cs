using HorsesForCourses.Core.Domain.Coaches;
using HorsesForCourses.Core.Domain.Coaches.InvalidationReasons;


namespace HorsesForCourses.Tests.Documentation.Coaches.A_RegisterCoach;

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
    public void RegisterCoach_WithEmptyName_ShouldThrow()
        => Assert.Throws<CoachNameCanNotBeEmpty>(
            () => new Coach(string.Empty, TheCannonical.CoachEmail));

    [Fact]
    public void RegisterCoach_WithEmptyEmail_ShouldThrow()
        => Assert.Throws<CoachEmailCanNotBeEmpty>(() => new Coach(TheCannonical.CoachName, string.Empty));
}
