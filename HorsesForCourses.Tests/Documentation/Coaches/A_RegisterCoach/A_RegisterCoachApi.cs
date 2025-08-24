using HorsesForCourses.Api.Coaches.RegisterCoach;
using Microsoft.AspNetCore.Mvc;
using QuickPulse.Explains;

namespace HorsesForCourses.Tests.Documentation.Coaches.A_RegisterCoach;

[DocFile]
[DocFileHeader("Api")]
[DocCode("POST /coaches", "bash")]
public class A_RegisterCoachApi : CoachesControllerTests
{
    private readonly RegisterCoachRequest request;

    public A_RegisterCoachApi()
    {
        request = new RegisterCoachRequest(TheCannonical.CoachName, TheCannonical.CoachEmail);
    }

    private async Task<OkObjectResult?> Act()
    {
        return await controller.RegisterCoach(request)! as OkObjectResult;
    }

    [Fact]
    [DocHeader("Request JSON:")]
    [DocCode(
  @"{
    ""name"": ""Alice"",
    ""email"": ""alice@example.com""
  }", "json")]
    public async Task RegisterCoach_delivers_the_coach_request_as_coach_to_the_supervisor()
    {
        await Act();
        supervisor.Verify(a => a.Enlist(TheCannonical.CheckCoachNameAndEmail));
        supervisor.Verify(a => a.Ship());
    }

    [Fact]
    public async Task RegisterCoach_ReturnsOk_WithValidId()
    {
        var result = await Act();
        Assert.NotNull(result);
        Assert.IsType<int>(result!.Value);
    }
}