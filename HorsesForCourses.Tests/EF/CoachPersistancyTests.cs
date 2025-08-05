using HorsesForCourses.Core.Domain;
using HorsesForCourses.WebApi;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;

namespace HorsesForCourses.Tests.EF;

public class CoachPersistancyTests
{
    [Fact]
    public async Task ShouldPersistData()
    {
        var connection = new SqliteConnection("DataSource=:memory:");
        await connection.OpenAsync();
        var options = new DbContextOptionsBuilder<AppDBContext>()
            .UseSqlite(connection)
            .Options;

        using (var context = new AppDBContext(options))
        {
            await context.Database.EnsureCreatedAsync();
        }

        using (var context = new AppDBContext(options))
        {
            context.Coaches.Add(new Coach("naam", "em@il"));
            await context.SaveChangesAsync();
        }

        using (var context = new AppDBContext(options))
        {
            var coach = await context.Coaches.FindAsync(1);

            Assert.NotNull(coach);
            Assert.Equal("naam", coach!.Name);
        }
    }
}