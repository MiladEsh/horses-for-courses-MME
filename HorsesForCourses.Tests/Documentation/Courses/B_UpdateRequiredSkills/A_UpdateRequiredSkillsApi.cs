using HorsesForCourses.Tests.Documentation.Courses;
using Microsoft.AspNetCore.Mvc;
using Moq;

namespace HorsesForCourses.Tests.Documentation.Courses.B_UpdateRequiredSkills;


public class A_UpdateRequiredSkillsApi : CoursesControllerTests
{
    private readonly List<string> request = ["one", "two"];

    [Fact]
    public async Task UpdateRequiredSkills_uses_the_query_object()
    {
        var response = await controller.UpdateRequiredSkills(42, request);
        query.Verify(a => a.Load(42));
    }

    [Fact]
    public async Task UpdateRequiredSkills_calls_update_skills()
    {
        await controller.UpdateRequiredSkills(42, request);
        Assert.True(spy.SkillsCalled);
        Assert.Equal(request, spy.SkillsSeen);
    }

    [Fact]
    public async Task UpdateRequiredSkills_does_not_call_supervisor_ship()
    {
        await controller.UpdateRequiredSkills(42, request);
        supervisor.Verify(a => a.Ship(), Times.Never);
    }

    [Fact]
    public async Task UpdateRequiredSkills_Returns_NoContent()
    {
        var response = await controller.UpdateRequiredSkills(42, request);
        Assert.IsType<NoContentResult>(response);
    }
}