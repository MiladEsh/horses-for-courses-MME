using HorsesForCourses.Core.Abstractions;
using HorsesForCourses.Core.Domain;
using HorsesForCourses.Core.Domain.Coaches;
using HorsesForCourses.WebApi;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;

namespace HorsesForCourses.Tests.EF;

public class CoachPersistancyTests
{
    [Fact]
    public async Task ShouldPersistData()
    {
        await using var connection = new SqliteConnection("DataSource=:memory:");
        await connection.OpenAsync();
        var options = new DbContextOptionsBuilder<AppDBContext>()
            .UseSqlite(connection)
            .Options;
        var id = 0;
        using (var context = new AppDBContext(options))
        {
            await context.Database.EnsureCreatedAsync();
        }

        using (var context = new AppDBContext(options))
        {
            var coach = new Coach("naam", "em@il");
            id = coach.Id.Value;
            coach.AddSkill(Skill.From("whatever"));
            context.Coaches.Add(coach);
            await context.SaveChangesAsync();
        }

        using (var context = new AppDBContext(options))
        {
            var coach = await context.Coaches.FindAsync(Id<Coach>.From(id));

            Assert.NotNull(coach);
            Assert.Equal("naam", coach!.Name);
            Assert.Equal("whatever", coach!.Skills.First().Value);
        }
    }
}