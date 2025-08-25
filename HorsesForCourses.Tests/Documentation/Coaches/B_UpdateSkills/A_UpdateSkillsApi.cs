using HorsesForCourses.Api.Coaches.UpdateSkills;
using HorsesForCourses.Tests.Tools.Coaches;
using Microsoft.AspNetCore.Mvc;
using Moq;


namespace HorsesForCourses.Tests.Documentation.Coaches.B_UpdateSkills;

public class A_UpdateSkillsApi : CoachesControllerTests
{
    private readonly UpdateSkillsRequest request;

    public A_UpdateSkillsApi()
    {
        request = new UpdateSkillsRequest(["one", "two"]);
    }

    [Fact]
    public async Task UpdateSkills_uses_the_query_object()
    {
        var response = await controller.UpdateSkills(42, request);
        coachQuery.Verify(a => a.Load(42));
    }

    [Fact]
    public async Task UpdateSkills_calls_update_skills()
    {
        await controller.UpdateSkills(42, request);
        Assert.True(spy.Called);
        Assert.Equal(request.Skills, spy.Seen);
    }

    [Fact]
    public async Task UpdateSkills_calls_supervisor_ship()
    {
        await controller.UpdateSkills(42, request);
        supervisor.Verify(a => a.Ship());
    }

    [Fact]
    public async Task UpdateSkills_ReturnsOk_WithValidId()
    {
        var response = await controller.UpdateSkills(42, request);
        Assert.IsType<NoContentResult>(response);
    }
}