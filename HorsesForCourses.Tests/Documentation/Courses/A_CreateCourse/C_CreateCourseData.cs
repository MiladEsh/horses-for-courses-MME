using HorsesForCourses.Api.Warehouse;
using HorsesForCourses.Core.Abstractions;
using HorsesForCourses.Core.Domain.Courses;


namespace HorsesForCourses.Tests.Documentation.Courses.A_CreateCourse;

public class C_CreateCourseData : TheDatabaseTest
{
    private readonly DataSupervisor supervisor;
    private readonly Course course;

    public C_CreateCourseData()
    {
        supervisor = new DataSupervisor(GetDbContext());
        course = TheCannonical.Course();
    }

    private async Task Act()
    {
        await supervisor.Enlist(course);
        await supervisor.Ship();
    }

    private Course Reload()
        => GetDbContext().Courses.Find(Id<Course>.From(course.Id.Value))!;

    [Fact]
    public async Task Supervisor_Assigns_id()
    {
        await Act();
        Assert.NotEqual(default, course.Id.Value);
    }

    [Fact]
    public async Task Supervisor_Stores()
    {
        await Act();
        var reloaded = Reload();
        Assert.NotNull(reloaded);
        Assert.NotEqual(default, reloaded.Id.Value);
    }

    [Fact]
    public async Task Supervisor_name_and_email()
    {
        await Act();
        var reloaded = Reload();
        Assert.Equal(TheCannonical.CourseName, reloaded!.Name);
        Assert.Equal(TheCannonical.CourseStart, reloaded!.StartDate);
        Assert.Equal(TheCannonical.CourseEnd, reloaded!.EndDate);
    }
}
