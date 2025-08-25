using HorsesForCourses.Core.Domain.Courses;
using HorsesForCourses.Core.Domain.Courses.InvalidationReasons;
using HorsesForCourses.Tests.Tools;
using HorsesForCourses.Tests.Tools.Courses;


namespace HorsesForCourses.Tests.Documentation.Courses.E_AssignCoach;

public class B_AssignCoachDomain : CourseDomainTests
{
    protected override Course ManipulateEntity(Course entity)
        => entity.UpdateTimeSlots(TheCannonical.TimeSlotsFullDayMonday());

    [Fact]
    public void AssignCoach_WithValidData_ShouldSucceed()
    {
        Entity.Confirm().AssignCoach(TheCannonical.Coach());
        Assert.NotNull(Entity.AssignedCoach);
        Assert.Equal(TheCannonical.CoachName, Entity.AssignedCoach.Name);
    }

    [Fact]
    public void AssignCoach_When_Unconfirmed_Throws()
    {
        Assert.Throws<CourseNotYetConfirmed>(() => Entity.AssignCoach(TheCannonical.Coach()));
    }

    [Fact]
    public void AssignCoach_Twice_Throws()
    {
        Entity.Confirm().AssignCoach(TheCannonical.Coach());
        Assert.Throws<CourseAlreadyHasCoach>(() => Entity.AssignCoach(TheCannonical.Coach()));
    }

    [Fact]
    public void Coach_Lacking_Skill_Throws()
    {
        Entity.UpdateRequiredSkills(["not this one"]).Confirm();
        Assert.Throws<CoachNotSuitableForCourse>(() => Entity.AssignCoach(TheCannonical.Coach()));
    }

    [Fact]
    public void CoachUnavalable_Throws()
    {
        var coach = TheCannonical.Coach();
        var course = TheCannonical.Course()
            .UpdateTimeSlots(TheCannonical.TimeSlotsFullDayMonday())
            .Confirm()
            .AssignCoach(coach);
        Entity.Confirm();
        Assert.Throws<CoachNotAvailableForCourse>(() => Entity.AssignCoach(coach));
    }

    [Fact]
    public void AssignCoach_Without_TimeSlots_Throws()
    {
        Entity.UpdateTimeSlots([]);
        Assert.Throws<AtLeastOneTimeSlotRequired>(() => Entity.Confirm());
    }
}
