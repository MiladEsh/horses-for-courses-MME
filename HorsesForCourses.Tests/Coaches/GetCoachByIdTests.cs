using HorsesForCourses.Api.Coaches;
using HorsesForCourses.Core.Domain.Coaches;
using HorsesForCourses.Tests.Tools;


namespace HorsesForCourses.Tests.Coaches;


public class GetCoachByIdTests : TheDatabaseTest
{
    private async Task<Coach?> Act()
        => await new GetCoachById(GetDbContext()).Load(1);

    [Fact]
    public async Task LoadIt()
    {
        AddToDb(TheCanonical.Coach());
        var result = await Act();
        Assert.NotNull(result);
        Assert.Equal(1, result.Id.Value);
        Assert.Equal(TheCanonical.CoachName, result.Name);
    }
}