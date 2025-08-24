using HorsesForCourses.Api.Coaches;
using HorsesForCourses.Api.Coaches.RegisterCoach;
using HorsesForCourses.Api.Warehouse;
using HorsesForCourses.Core.Domain.Coaches;
using Microsoft.AspNetCore.Mvc;
using Moq;
using QuickPulse.Explains;

namespace HorsesForCourses.Tests.Documentation.A_RegisterCoach;

[DocFile]
[DocFileHeader("Api")]
[DocCode("POST /coaches", "bash")]
public class A_RegisterCoachApi
{
  private readonly Mock<IAmASuperVisor> supervisor;
  private readonly CoachesController controller;

  public A_RegisterCoachApi()
  {
    supervisor = new Mock<IAmASuperVisor>();
    controller = new CoachesController(supervisor.Object, null!);
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
    var request = new RegisterCoachRequest("a", "a@a.a");

    var response = await controller.RegisterCoach(request);
    var result = response as OkObjectResult;

    supervisor.Verify(a => a.Enlist(It.Is<Coach>(a => a.Name == "a" && a.Email == "a@a.a")));
    supervisor.Verify(a => a.Ship());
  }

  [Fact]
  public async Task RegisterCoach_ReturnsOk_WithValidId()
  {
    var request = new RegisterCoachRequest("a", "a@a.a");

    var response = await controller.RegisterCoach(request);
    var result = response as OkObjectResult;

    Assert.NotNull(result);
    Assert.IsType<int>(result!.Value);
  }
}