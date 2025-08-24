using HorsesForCourses.Api.Warehouse;
using HorsesForCourses.Core.Abstractions;
using HorsesForCourses.Core.Domain.Coaches;
using QuickPulse.Explains;

namespace HorsesForCourses.Tests.Documentation.Coaches.A_RegisterCoach;

[DocFile]
[DocFileHeader("Data")]
public class C_RegisterCoachData : TheDatabaseTest
{
    private readonly DataSupervisor supervisor;
    private readonly Coach coach;

    public C_RegisterCoachData()
    {
        supervisor = new DataSupervisor(GetDbContext());
        coach = TheCannonical.Coach();
    }

    private async Task Act()
    {
        await supervisor.Enlist(coach);
        await supervisor.Ship();
    }

    private Coach Reload()
        => GetDbContext().Coaches.Find(Id<Coach>.From(coach.Id.Value))!;

    [Fact]
    [DocContent("The database assigns the id.")]
    public async Task Supervisor_Assigns_id()
    {
        await Act();
        Assert.NotEqual(default, coach.Id.Value);
    }

    [Fact]
    [DocContent("The coach is stored in the database.")]
    public async Task Supervisor_Stores()
    {
        await Act();
        var reloaded = Reload();
        Assert.NotNull(reloaded);
        Assert.NotEqual(default, reloaded.Id.Value);
    }

    [Fact]
    [DocContent("Name and email are taken from the request data.")]
    public async Task Supervisor_name_and_email()
    {
        await Act();
        var reloaded = Reload();
        Assert.Equal(TheCannonical.CoachName, reloaded!.Name);
        Assert.Equal(TheCannonical.CoachEmail, reloaded!.Email);
    }
}
