using HorsesForCourses.Api.Warehouse;
using HorsesForCourses.Core.Abstractions;
using HorsesForCourses.Core.Domain.Coaches;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using QuickPulse.Explains;
using QuickPulse.Show;

namespace HorsesForCourses.Tests.Documentation.A_RegisterCoach;

[DocFile]
[DocFileHeader("Data")]
public class C_RegisterCoachData
{
    private readonly DbContextOptions<AppDBContext> options;
    private AppDBContext GetContext() => new(options);

    private readonly IAmASuperVisor supervisor;

    public C_RegisterCoachData()
    {
        var connection = new SqliteConnection("DataSource=:memory:");
        connection.Open();
        var builder = new DbContextOptionsBuilder<AppDBContext>()
            .UseSqlite(connection);
        // builder.LogTo(s => s.PulseToLog("sql.log"), Microsoft.Extensions.Logging.LogLevel.Information);
        // builder.EnableSensitiveDataLogging();
        options = builder.Options;
        var dbContext = GetContext();
        dbContext.Database.EnsureCreated();
        supervisor = new DataSupervisor(dbContext);
    }

    [Fact]
    [DocContent("The coach is stored in the database.")]
    public async Task Supervisor_Stores()
    {
        var coach = new Coach("a", "a@a.a");
        await supervisor.Enlist(coach);
        await supervisor.Ship();

        using var dbContext = new AppDBContext(options);
        var reloaded = dbContext.Coaches.Find(Id<Coach>.From(coach.Id.Value));

        Assert.NotNull(coach);
    }

    [Fact]
    [DocContent("The database assigns the id.")]
    public async Task Supervisor_Assigns_id()
    {
        var coach = new Coach("a", "a@a.a");

        await supervisor.Enlist(coach);
        await supervisor.Ship();

        using var dbContext = new AppDBContext(options);
        var reloaded = dbContext.Coaches.Find(Id<Coach>.From(coach.Id.Value));

        Assert.NotEqual(default, reloaded!.Id.Value);
    }

    [Fact]
    [DocContent("Name and email are taken from the request data.")]
    public async Task Supervisor_name_and_email()
    {
        var coach = new Coach("a", "a@a.a");
        await supervisor.Enlist(coach);
        await supervisor.Ship();

        using var dbContext = new AppDBContext(options);
        var reloaded = dbContext.Coaches.Find(Id<Coach>.From(coach.Id.Value));

        Assert.Equal("a", reloaded!.Name);
        Assert.Equal("a@a.a", reloaded!.Email);
    }
}
