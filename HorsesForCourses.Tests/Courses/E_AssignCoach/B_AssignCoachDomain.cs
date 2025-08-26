using HorsesForCourses.Core.Domain;
using HorsesForCourses.Core.Domain.Courses;
using HorsesForCourses.Core.Domain.Courses.InvalidationReasons;
using HorsesForCourses.Tests.Tools;
using HorsesForCourses.Tests.Tools.Courses;
using WibblyWobbly;


namespace HorsesForCourses.Tests.Courses.E_AssignCoach;

public class B_AssignCoachDomain : CourseDomainTests
{
    [Fact]
    public void AssignCoach_WithValidData_ShouldSucceed()
    {
        Entity
            .UpdateTimeSlots(TheCanonical.TimeSlotsFullDayMonday())
            .Confirm()
            .AssignCoach(TheCanonical.Coach());
        Assert.NotNull(Entity.AssignedCoach);
        Assert.Equal(TheCanonical.CoachName, Entity.AssignedCoach.Name);
    }

    [Fact]
    public void AssignCoach_Without_TimeSlots_Throws()
    {
        Assert.Throws<AtLeastOneTimeSlotRequired>(() => Entity.Confirm());
    }

    [Fact]
    public void AssignCoach_When_Unconfirmed_Throws()
        => Assert.Throws<CourseNotYetConfirmed>(() =>
            Entity
                .UpdateTimeSlots(TheCanonical.TimeSlotsFullDayMonday())
                .AssignCoach(TheCanonical.Coach()));

    [Fact]
    public void AssignCoach_Twice_Throws()
    {
        Entity
            .UpdateTimeSlots(TheCanonical.TimeSlotsFullDayMonday())
            .Confirm().AssignCoach(TheCanonical.Coach());
        Assert.Throws<CourseAlreadyHasCoach>(() => Entity.AssignCoach(TheCanonical.Coach()));
    }

    [Fact]
    public void Coach_Lacking_Skill_Throws()
    {
        Entity
            .UpdateRequiredSkills(["not this one"])
            .UpdateTimeSlots(TheCanonical.TimeSlotsFullDayMonday())
            .Confirm();
        Assert.Throws<CoachNotSuitableForCourse>(() => Entity.AssignCoach(TheCanonical.Coach()));
    }

    [Fact]
    public void CoachUnavailable_Throws()
    {
        var coach = TheCanonical.Coach();
        var course = TheCanonical.Course()
            .UpdateTimeSlots(TheCanonical.TimeSlotsFullDayMonday())
            .Confirm()
            .AssignCoach(coach);
        Entity.UpdateTimeSlots(TheCanonical.TimeSlotsFullDayMonday()).Confirm();
        Assert.Throws<CoachNotAvailableForCourse>(() => Entity.AssignCoach(coach));
    }

    [Fact]
    public void CoachUnavailable_Case_1_Succeeds()
    {
        var coach = TheCanonical.Coach();
        new Course("Assigned", DateOnly.FromDateTime(19.August(2025)), DateOnly.FromDateTime(19.August(2025)))
            .UpdateTimeSlots([TimeSlot.From(CourseDay.Monday, OfficeHour.From(9), OfficeHour.From(17))])
            .Confirm()
            .AssignCoach(coach);
        var otherCourse = new Course("To Assign", DateOnly.FromDateTime(19.August(2025)), DateOnly.FromDateTime(19.August(2025)))
            .UpdateTimeSlots([TimeSlot.From(CourseDay.Monday, OfficeHour.From(9), OfficeHour.From(17))])
            .Confirm();
        Assert.Equal(DayOfWeek.Tuesday, 19.August(2025).DayOfWeek);
        otherCourse.AssignCoach(coach);
    }

    [Fact]
    public void CoachUnavailable_Case_2_Succeeds()
    {
        var coach = TheCanonical.Coach();
        new Course("Assigned",
                DateOnly.FromDateTime(19.August(2025)),
                DateOnly.FromDateTime(19.August(2025)))
            .UpdateTimeSlots([TimeSlot.From(CourseDay.Tuesday, OfficeHour.From(9), OfficeHour.From(17))])
            .Confirm()
            .AssignCoach(coach);
        var otherCourse = new Course("To Assign",
                DateOnly.FromDateTime(20.August(2025)),
                DateOnly.FromDateTime(25.August(2025)))
            .UpdateTimeSlots([TimeSlot.From(CourseDay.Tuesday, OfficeHour.From(9), OfficeHour.From(17))])
            .Confirm();
        Assert.Equal(DayOfWeek.Tuesday, 19.August(2025).DayOfWeek);
        otherCourse.AssignCoach(coach);
    }

    [Fact]
    public void CoachUnavailable_Case_3_Succeeds()
    {
        var coach = TheCanonical.Coach();
        new Course("Assigned",
                DateOnly.FromDateTime(19.August(2025)),
                DateOnly.FromDateTime(22.August(2025)))
            .UpdateTimeSlots([TimeSlot.From(CourseDay.Tuesday, OfficeHour.From(9), OfficeHour.From(17))])
            .Confirm()
            .AssignCoach(coach);
        var otherCourse = new Course("To Assign",
                DateOnly.FromDateTime(20.August(2025)),
                DateOnly.FromDateTime(30.August(2025)))
            .UpdateTimeSlots([TimeSlot.From(CourseDay.Tuesday, OfficeHour.From(9), OfficeHour.From(17))])
            .Confirm();
        Assert.Equal(DayOfWeek.Tuesday, 19.August(2025).DayOfWeek);
        Assert.Equal(DayOfWeek.Tuesday, 26.August(2025).DayOfWeek);
        otherCourse.AssignCoach(coach);
    }
}
