using HorsesForCourses.Tests.Tools.Courses;
using Microsoft.AspNetCore.Mvc;

namespace HorsesForCourses.Tests.Courses.B_UpdateRequiredSkills;


public class A_UpdateRequiredSkillsApi : CoursesControllerTests
{
    private readonly List<string> request = ["one", "two"];

    [Fact]
    public async Task UpdateRequiredSkills_uses_the_query_object()
    {
        var response = await controller.UpdateRequiredSkills(1, request);
        getCourseById.Verify(a => a.Load(1));
    }

    [Fact]
    public async Task UpdateRequiredSkills_calls_update_skills()
    {
        await controller.UpdateRequiredSkills(1, request);
        Assert.True(spy.SkillsCalled);
        Assert.Equal(request, spy.SkillsSeen);
    }

    [Fact]
    public async Task UpdateRequiredSkills_calls_supervisor_ship()
    {
        await controller.UpdateRequiredSkills(1, request);
        supervisor.Verify(a => a.Ship());
    }

    [Fact]
    public async Task UpdateRequiredSkills_Returns_NoContent()
    {
        var response = await controller.UpdateRequiredSkills(1, request);
        Assert.IsType<NoContentResult>(response);
    }

    [Fact]
    public async Task UpdateRequiredSkills_Returns_Not_Found_If_No_Course()
    {
        var response = await controller.UpdateRequiredSkills(-1, request);
        Assert.IsType<NotFoundResult>(response);
    }
}