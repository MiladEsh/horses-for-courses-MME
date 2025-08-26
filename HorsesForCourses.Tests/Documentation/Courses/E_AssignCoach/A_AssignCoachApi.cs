using HorsesForCourses.Api.Courses.AssignCoach;
using HorsesForCourses.Core.Domain.Courses;
using HorsesForCourses.Tests.Tools;
using HorsesForCourses.Tests.Tools.Courses;
using Microsoft.AspNetCore.Mvc;
using Moq;

namespace HorsesForCourses.Tests.Documentation.Courses.E_AssignCoach;


public class A_AssignCoachApi : CoursesControllerTests
{
    private AssignCoachRequest request = new(666);

    protected override void ManipulateEntity(Course entity)
    {
        entity.UpdateTimeSlots(TheCannonical.TimeSlotsFullDayMonday());
        entity.Confirm();
    }

    [Fact]
    public async Task AssignCoach_uses_the_query_objects()
    {
        await controller.AssignCoach(42, request);
        getCourseById.Verify(a => a.Load(42));
        getCoachById.Verify(a => a.Load(666));
    }

    [Fact]
    public async Task AssignCoach_calls_domain()
    {
        await controller.AssignCoach(42, request);
        Assert.True(spy.AssignCoachCalled);
        Assert.Equal("a", spy.AssignCoachSeen!.Name);
    }

    [Fact]
    public async Task AssignCoach_calls_supervisor_ship()
    {
        await controller.AssignCoach(42, request);
        supervisor.Verify(a => a.Ship());
    }

    [Fact]
    public async Task AssignCoach_NoContent()
    {
        var response = await controller.AssignCoach(42, request);
        Assert.IsType<NoContentResult>(response);
    }
}