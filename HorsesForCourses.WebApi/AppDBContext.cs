using HorsesForCourses.Core.Abstractions;
using HorsesForCourses.Core.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace HorsesForCourses.WebApi;

public class AppDBContext : DbContext
{
    public DbSet<Coach> Coaches { get; set; }
    public DbSet<Skill> Skills { get; set; }
    public DbSet<Course> Courses { get; set; }

    public AppDBContext(DbContextOptions<AppDBContext> options) : base(options) { }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.ApplyConfiguration(new CoachConfiguration());
        modelBuilder.ApplyConfiguration(new CourseConfiguration());
    }
}

public class IdValueConverter<T> : ValueConverter<Id<T>, Guid>
{
    public IdValueConverter()
        : base(
            id => id.Value,
            value => Id<T>.From(value)) // Use the factory method
    { }
}
