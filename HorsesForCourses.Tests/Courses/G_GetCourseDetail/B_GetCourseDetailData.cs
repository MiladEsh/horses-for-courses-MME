using HorsesForCourses.Api.Courses.GetCourseDetail;
using HorsesForCourses.Tests.Tools;


namespace HorsesForCourses.Tests.Courses.G_GetCourseDetail;


public class B_GetCourseDetailData : TheDatabaseTest
{
    private async Task<CourseDetail?> Act()
        => await new GetCourseDetail(GetDbContext()).One(1);

    [Fact]
    public async Task With_Course()
    {
        AddToDb(TheCannonical.Course());
        var detail = await Act();
        Assert.NotNull(detail);
        Assert.Equal(1, detail.Id);
        Assert.Equal(TheCannonical.CourseName, detail.Name);
        Assert.Equal(TheCannonical.CourseStart, detail.Start);
        Assert.Equal(TheCannonical.CourseEnd, detail.End);
        Assert.Equal([], detail.Skills);
        Assert.Null(detail.Coach);
    }
}