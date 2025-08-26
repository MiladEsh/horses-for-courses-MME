using HorsesForCourses.Core.Domain.Courses;
using HorsesForCourses.Tests.Tools;
using HorsesForCourses.Tests.Tools.Courses;
using Microsoft.AspNetCore.Mvc;
using Moq;

namespace HorsesForCourses.Tests.Documentation.Courses.D_ConfirmCourse;


public class A_UpdateConfirmCourseApi : CoursesControllerTests
{
    private readonly List<string> request = ["one", "two"];

    protected override void ManipulateEntity(Course entity)
    {
        entity.UpdateTimeSlots(TheCannonical.TimeSlotsFullDayMonday());
    }

    [Fact]
    public async Task UpdateConfirmCourse_uses_the_query_object()
    {
        await controller.ConfirmCourse(42);
        getCourseById.Verify(a => a.Load(42));
    }

    [Fact]
    public async Task UpdateConfirmCourse_calls_confirm()
    {
        await controller.ConfirmCourse(42);
        Assert.True(spy.IsConfirmed);
    }

    [Fact]
    public async Task UpdateConfirmCourse_calls_supervisor_ship()
    {
        await controller.ConfirmCourse(42);
        supervisor.Verify(a => a.Ship());
    }

    [Fact]
    public async Task UpdateConfirmCourse_NoContent()
    {
        var response = await controller.ConfirmCourse(42);
        Assert.IsType<NoContentResult>(response);
    }
}