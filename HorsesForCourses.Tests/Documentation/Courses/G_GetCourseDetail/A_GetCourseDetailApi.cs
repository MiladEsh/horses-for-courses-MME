using HorsesForCourses.Api.Courses.GetCourseDetail;
using HorsesForCourses.Tests.Tools.Courses;
using Microsoft.AspNetCore.Mvc;
using Moq;

namespace HorsesForCourses.Tests.Documentation.Courses.G_GetCourseDetail;

public class A_GetCourseDetailApi : CoursesControllerTests
{
    private async Task<OkObjectResult?> Act()
    {
        return await controller.GetCourseDetail(42) as OkObjectResult;
    }

    [Fact]
    public async Task GetCourseDetail_uses_the_query_object()
    {
        var result = await Act();
        getCourseDetail.Verify(a => a.One(It.IsAny<int>()));
    }

    [Fact]
    public async Task GetCourseDetailReturnsOk_With_List()
    {
        var result = await Act();
        Assert.NotNull(result);
        Assert.IsType<CourseDetail>(result!.Value);
    }
}