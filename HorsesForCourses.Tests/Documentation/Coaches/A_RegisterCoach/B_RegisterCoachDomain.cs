using HorsesForCourses.Core.Domain.Coaches;
using HorsesForCourses.Core.Domain.Coaches.InvalidationReasons;


namespace HorsesForCourses.Tests.Documentation.Coaches.A_RegisterCoach;

public class B_RegisterCoachDomain : CoachDomainTests
{
    [Fact]
    public void RegisterCoach_WithValidData_ShouldSucceed()
    {
        Assert.Equal(TheCannonical.CoachName, Entity.Name);
        Assert.Equal(TheCannonical.CoachEmail, Entity.Email);
        Assert.Empty(Entity.Skills);
    }

    [Fact]
    public void RegisterCoach_WithValidData_keeps_default_id()
    {
        Assert.Equal(default, Entity.Id.Value);
    }

    [Fact]
    public void RegisterCoach_WithEmptyName_ShouldThrow()
        => Assert.Throws<CoachNameCanNotBeEmpty>(
            () => new Coach(string.Empty, TheCannonical.CoachEmail));

    [Fact]
    public void RegisterCoach_WithEmptyEmail_ShouldThrow()
        => Assert.Throws<CoachEmailCanNotBeEmpty>(() => new Coach(TheCannonical.CoachName, string.Empty));


}
