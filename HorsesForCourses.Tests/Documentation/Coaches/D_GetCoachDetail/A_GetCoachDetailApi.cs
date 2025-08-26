using HorsesForCourses.Api.Coaches.GetCoaches;
using HorsesForCourses.Tests.Tools.Coaches;
using Microsoft.AspNetCore.Mvc;
using Moq;

namespace HorsesForCourses.Tests.Documentation.Coaches.D_GetCoachDetail;

public class A_GetCoachDetailApi : CoachesControllerTests
{
    private async Task<OkObjectResult?> Act()
    {
        return await controller.GetCoachDetail(42) as OkObjectResult;
    }

    [Fact]
    public async Task GetCoachDetail_uses_the_query_object()
    {
        var result = await Act();
        getCoachDetail.Verify(a => a.One(It.IsAny<int>()));
    }

    [Fact]
    public async Task GetCoachDetailReturnsOk_With_List()
    {
        var result = await Act();
        Assert.NotNull(result);
        Assert.IsType<CoachDetail>(result!.Value);
    }
}