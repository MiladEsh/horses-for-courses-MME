using HorsesForCourses.Api.Coaches.GetCoaches;
using HorsesForCourses.Tests.Tools;


namespace HorsesForCourses.Tests.Coaches.D_GetCoachDetail;


public class B_GetCoachDetailData : TheDatabaseTest
{
    private async Task<CoachDetail?> Act()
        => await new GetCoachDetail(GetDbContext()).One(1);

    [Fact]
    public async Task With_Coach()
    {
        AddToDb(TheCannonical.Coach());
        var detail = await Act();
        Assert.NotNull(detail);
        Assert.Equal(1, detail.Id);
        Assert.Equal(TheCannonical.CoachName, detail.Name);
        Assert.Equal(TheCannonical.CoachEmail, detail.Email);
        Assert.Equal([], detail.Skills);
        Assert.Equal([], detail.Courses);
    }
}