using HorsesForCourses.Api.Courses.AssignCoach;
using HorsesForCourses.Core.Domain.Courses;
using HorsesForCourses.Tests.Tools;
using HorsesForCourses.Tests.Tools.Courses;
using Microsoft.AspNetCore.Mvc;

namespace HorsesForCourses.Tests.Courses.E_AssignCoach;


public class A_AssignCoachApi : CoursesControllerTests
{
    protected override void ManipulateEntity(Course entity)
    {
        entity.UpdateTimeSlots(TheCannonical.TimeSlotsFullDayMonday());
        entity.Confirm();
    }

    [Fact]
    public async Task AssignCoach_uses_the_query_objects()
    {
        await controller.AssignCoach(1, new AssignCoachRequest(1));
        getCourseById.Verify(a => a.Load(1));
        getCoachById.Verify(a => a.Load(1));
    }

    [Fact]
    public async Task AssignCoach_calls_domain()
    {
        await controller.AssignCoach(1, new AssignCoachRequest(1));
        Assert.True(spy.AssignCoachCalled);
        Assert.Equal("a", spy.AssignCoachSeen!.Name);
    }

    [Fact]
    public async Task AssignCoach_calls_supervisor_ship()
    {
        await controller.AssignCoach(1, new AssignCoachRequest(1));
        supervisor.Verify(a => a.Ship());
    }

    [Fact]
    public async Task AssignCoach_NoContent()
    {
        var response = await controller.AssignCoach(1, new AssignCoachRequest(1));
        Assert.IsType<NoContentResult>(response);
    }

    [Fact]
    public async Task UpdateTimeSlots_Returns_Not_Found_If_No_Course()
    {
        var response = await controller.AssignCoach(-1, new AssignCoachRequest(1));
        Assert.IsType<NotFoundResult>(response);
    }

    [Fact]
    public async Task UpdateTimeSlots_Returns_Not_Found_If_No_Coach()
    {
        var response = await controller.AssignCoach(1, new AssignCoachRequest(-1));
        Assert.IsType<NotFoundResult>(response);
    }
}