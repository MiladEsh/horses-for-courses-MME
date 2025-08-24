using HorsesForCourses.Api.Warehouse;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;

namespace HorsesForCourses.Tests;

public abstract class TheDatabaseTest
{
    protected readonly DbContextOptions<AppDbContext> Options;
    protected AppDbContext GetDbContext() => new(Options);

    public TheDatabaseTest()
    {
        var connection = new SqliteConnection("DataSource=:memory:");
        connection.Open();
        var builder = new DbContextOptionsBuilder<AppDbContext>().UseSqlite(connection);
        Options = builder.Options;
        var dbContext = GetDbContext();
        dbContext.Database.EnsureCreated();
    }
    // If we need logging of sql statements
    // builder.LogTo(s => s.PulseToLog("sql.log"), Microsoft.Extensions.Logging.LogLevel.Information);
    // builder.EnableSensitiveDataLogging();

    protected void AddToDb<T>(T entity)
    {
        var context = GetDbContext();
        context.Add(entity!);
        context.SaveChanges();
    }
}
