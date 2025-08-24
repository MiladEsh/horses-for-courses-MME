using HorsesForCourses.Api.Coaches;
using HorsesForCourses.Api.Coaches.RegisterCoach;
using HorsesForCourses.Api.Warehouse;
using HorsesForCourses.Core.Abstractions;
using HorsesForCourses.Core.Domain.Coaches;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using QuickAcid;
using QuickFuzzr;

namespace HorsesForCourses.Tests.Documentation.Coaches.A_RegisterCoach;

public class D_RegisterCoachIntegration
{
    private DbContextOptions<AppDbContext> GetOptions()
    {
        var connection = new SqliteConnection("DataSource=:memory:");
        connection.Open();
        var options = new DbContextOptionsBuilder<AppDbContext>()
            .UseSqlite(connection)
            .Options;
        GetContext(options).Database.EnsureCreated();
        return options;
    }

    private AppDbContext GetContext(DbContextOptions<AppDbContext> options) => new(options);

    [Fact]
    public void Supervisor_Stores()
    {
        var script =

            from options in "options".Stashed(() => GetOptions())
            from supervisor in "supervisor".Derived(() => new DataSupervisor(GetContext(options)))
            from controller in "controller".Derived(() => new CoachesController(supervisor, null!))

            let requestFuzzr =
                from _ in Fuzz.For<RegisterCoachRequest>().Construct(Fuzz.String(), Fuzz.String())
                from a in Fuzz.One<RegisterCoachRequest>()
                select a

            from request in "request".Input(requestFuzzr)
            from coachId in "act".Act(() => controller.RegisterCoach(request).GetOkResultValue<int>())
            from returnsId in "controller returns id".Spec(() => coachId != default)

            from reloaded in "reloaded".Derived(() => GetContext(options).Coaches.Find(Id<Coach>.From(coachId)))

            from reloadedIsNotNull in "reloaded Is Not Null".Spec(() => reloaded != null)
            from hasId in "has id".Spec(() => reloaded.Id.Value != 0)
            from hasNameSet in "has name set".Spec(() => reloaded.Name == request.Name)
            from hasEmailSet in "has name set".Spec(() => reloaded.Email == request.Email)

            select Acid.Test;

        QState.Run(script)
            .With(10.Runs())
            .AndOneExecutionPerRun();

    }
}


public static class ResultExtensions
{
    public static T GetOkResultValue<T>(this Task<IActionResult> result)
        => (T)(result.GetAwaiter().GetResult() as OkObjectResult)?.Value!;
}