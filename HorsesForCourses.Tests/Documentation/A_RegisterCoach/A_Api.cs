using HorsesForCourses.Api.Coaches;
using HorsesForCourses.Api.Warehouse;
using Microsoft.AspNetCore.Mvc;
using Moq;

namespace HorsesForCourses.Tests.Documentation.A_CreateCoach;

public class A_Api
{
  private readonly CoachesController controller;

  public A_Api()
  {
    var supervisor = new Mock<IAmASuperVisor>();
    controller = new CoachesController(supervisor.Object);
  }

  [Fact]
  public async Task RegisterCoach_ReturnsOk_WithValidId()
  {
    var request = new CoachesController.RegisterCoachRequest("Ben", "ben@coach.be");

    var response = await controller.RegisterCoach(request);
    var result = response as OkObjectResult;

    Assert.NotNull(result);
    Assert.IsType<int>(result!.Value);
  }

  [Fact]
  public async Task RegisterCoach_ReturnsProblem_When_request_is_null()
  {
    await Assert.ThrowsAsync<CoachesController.RegisterCoachRequestCanNotBeNull>(async () => await controller.RegisterCoach(null!));
  }
}