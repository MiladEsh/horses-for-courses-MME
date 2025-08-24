using HorsesForCourses.Api.Coaches;
using HorsesForCourses.Api.Coaches.RegisterCoach;
using HorsesForCourses.Api.Coaches.UpdateSkills;
using HorsesForCourses.Api.Warehouse;
using HorsesForCourses.Core.Domain.Coaches;
using Microsoft.AspNetCore.Mvc;
using Moq;
using QuickPulse.Explains;

namespace HorsesForCourses.Tests.Documentation.B_UpdateSkills;

[DocFile]
[DocFileHeader("Api")]
[DocCode("POST /coaches/{id}/skills", "bash")]
public class A_UpdateSkillsApi
{
    private readonly Mock<IAmASuperVisor> supervisor;
    private readonly Mock<IGetCoachForUpdateSkills> query;
    private readonly CoachesController controller;

    public A_UpdateSkillsApi()
    {
        supervisor = new Mock<IAmASuperVisor>();
        query = new Mock<IGetCoachForUpdateSkills>();
        controller = new CoachesController(supervisor.Object, query.Object);
    }

    [Fact]
    [DocHeader("Request JSON:")]
    [DocCode("{ \"skills\": [\"C#\", \"Agile\"] }", "json")]
    public async Task RegisterCoach_uses_the_query_object()
    {
        var request = new UpdateSkillsRequest(["one", "two"]);

        var response = await controller.UpdateSkills(42, request);

        query.Verify(a => a.Load(42));
    }

    [Fact]
    public async Task RegisterCoach_calls_update_skills()
    {
        var coach = new CoachSpy();
        var request = new UpdateSkillsRequest(["one", "two"]);
        query.Setup(a => a.Load(It.IsAny<int>())).ReturnsAsync(coach);

        var response = await controller.UpdateSkills(42, request);

        Assert.True(coach.Called);
        Assert.Equal(request.Skills, coach.Seen);
    }

    [Fact]
    public async Task RegisterCoach_does_not_call_supervisor_ship()
    {
        var request = new UpdateSkillsRequest(["one", "two"]);

        var response = await controller.UpdateSkills(42, request);

        supervisor.Verify(a => a.Ship(), Times.Never);
    }

    public class CoachSpy : Coach
    {
        public CoachSpy() : base("a", "a@a.a") { }
        public bool Called;
        public IEnumerable<string>? Seen;
        public override void UpdateSkills(IEnumerable<string> skills)
        {
            Called = true; Seen = skills;
        }
    }
}