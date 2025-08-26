using HorsesForCourses.Api.Courses.GetCourses;
using HorsesForCourses.Api.Warehouse.Paging;
using HorsesForCourses.Tests.Tools;


namespace HorsesForCourses.Tests.Documentation.Courses.F_GetCourses;


public class B_GetCoursesData : TheDatabaseTest
{
    private async Task<PagedResult<CourseSummary>> Act(PageRequest request)
        => await new GetCourseSummaries(GetDbContext()).All(request);

    [Fact]
    public async Task EmptyList()
    {
        var result = await Act(new PageRequest());
        Assert.Equal([], result.Items);
        Assert.Equal(0, result.TotalCount);
        Assert.Equal(1, result.PageNumber);
        Assert.Equal(25, result.PageSize);
        Assert.Equal(0, result.TotalPages);
        Assert.False(result.HasPrevious);
        Assert.False(result.HasNext);
    }

    [Fact]
    public async Task With_Course()
    {
        AddToDb(TheCannonical.Course());
        var result = await Act(new PageRequest());
        Assert.Single(result.Items);

        var summary = result.Items.Single();
        Assert.Equal(1, summary.Id);
        Assert.Equal(TheCannonical.CourseName, summary.Name);
        Assert.Equal(TheCannonical.CourseStart, summary.StartDate);
        Assert.Equal(TheCannonical.CourseEnd, summary.EndDate);
        Assert.False(summary.HasSchedule);
        Assert.False(summary.HasCoach);

        Assert.Equal(1, result.TotalCount);
        Assert.Equal(1, result.PageNumber);
        Assert.Equal(25, result.PageSize);
        Assert.Equal(1, result.TotalPages);
        Assert.False(result.HasPrevious);
        Assert.False(result.HasNext);
    }
}