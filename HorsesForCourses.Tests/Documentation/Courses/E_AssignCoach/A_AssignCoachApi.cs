using HorsesForCourses.Core.Domain.Courses;
using Microsoft.AspNetCore.Mvc;
using Moq;

namespace HorsesForCourses.Tests.Documentation.Courses.E_AssignCoach;


public class A_AssignCoachApi : CoursesControllerTests
{
    private readonly List<string> request = ["one", "two"];

    protected override void ManipulateEntity(Course entity)
    {
        entity.UpdateTimeSlots(TheCannonical.TimeSlotsFullDayMonday());
        entity.Confirm();
    }

    [Fact]
    public async Task AssignCoach_uses_the_query_objects()
    {
        await controller.AssignCoach(42, 666);
        courseQuery.Verify(a => a.Load(42));
        coachQuery.Verify(a => a.Load(666));
    }

    [Fact]
    public async Task AssignCoach_calls_domain()
    {
        await controller.AssignCoach(42, 666);
        Assert.True(spy.AssignCoachCalled);
        Assert.Equal("a", spy.AssignCoachSeen!.Name);
    }

    [Fact]
    public async Task AssignCoach_does_not_call_supervisor_ship()
    {
        await controller.AssignCoach(42, 666);
        supervisor.Verify(a => a.Ship(), Times.Never);
    }

    [Fact]
    public async Task AssignCoach_NoContent()
    {
        var response = await controller.AssignCoach(42, 666);
        Assert.IsType<NoContentResult>(response);
    }
}