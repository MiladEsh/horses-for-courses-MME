using HorsesForCourses.Core.Domain;
using HorsesForCourses.Core.Domain.Coaches;
using Microsoft.EntityFrameworkCore;

namespace HorsesForCourses.Api.Warehouse;

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
