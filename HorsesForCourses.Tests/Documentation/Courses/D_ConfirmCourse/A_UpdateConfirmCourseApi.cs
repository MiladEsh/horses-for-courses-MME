using Microsoft.AspNetCore.Mvc;
using Moq;

namespace HorsesForCourses.Tests.Documentation.Courses.B_UpdateConfirmCourse;


public class A_UpdateConfirmCourseApi : CoursesControllerTests
{
    private readonly List<string> request = ["one", "two"];

    [Fact]
    public async Task UpdateConfirmCourse_uses_the_query_object()
    {
        await controller.ConfirmCourse(42);
        query.Verify(a => a.Load(42));
    }

    [Fact]
    public async Task UpdateConfirmCourse_calls_update_skills()
    {
        await controller.ConfirmCourse(42);
        Assert.True(spy.Called);
        Assert.Equal(request, spy.Seen);
    }

    [Fact]
    public async Task UpdateConfirmCourse_does_not_call_supervisor_ship()
    {
        await controller.ConfirmCourse(42);
        supervisor.Verify(a => a.Ship(), Times.Never);
    }

    [Fact]
    public async Task UpdateConfirmCourse_ReturnsOk_WithValidId()
    {
        var response = await controller.ConfirmCourse(42);
        Assert.IsType<NoContentResult>(response);
    }
}