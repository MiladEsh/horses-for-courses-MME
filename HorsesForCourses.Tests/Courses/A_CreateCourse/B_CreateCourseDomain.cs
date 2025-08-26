using HorsesForCourses.Core.Domain.Courses;
using HorsesForCourses.Core.Domain.Courses.InvalidationReasons;
using HorsesForCourses.Tests.Tools;
using HorsesForCourses.Tests.Tools.Courses;


namespace HorsesForCourses.Tests.Courses.A_CreateCourse;

public class B_CreateCourseDomain : CourseDomainTests
{
    [Fact]
    public void CreateCourse_WithValidData_ShouldSucceed()
    {
        Assert.Equal(TheCannonical.CourseName, Entity.Name);
        Assert.Equal(TheCannonical.CourseStart, Entity.StartDate);
        Assert.Equal(TheCannonical.CourseEnd, Entity.EndDate);
    }

    [Fact]
    public void CreateCourse_WithValidData_keeps_default_id()
    {
        Assert.Equal(default, Entity.Id.Value);
    }

    [Fact]
    public void CreateCourse_WithEmptyName_ShouldThrow()
        => Assert.Throws<CourseNameCanNotBeEmpty>(
            () => new Course(string.Empty, TheCannonical.CourseStart, TheCannonical.CourseEnd));

    [Fact]
    public void CreateCourse_WithEndDateBeforeStartDate_ShouldThrow()
    {
        Assert.Throws<CourseEndDateCanNotBeBeforeStartDate>(() =>
            new Course(TheCannonical.CourseName, new DateOnly(2025, 7, 31), new DateOnly(2025, 7, 1))
        );
    }
}
