using HorsesForCourses.Core.Domain;
using HorsesForCourses.Core.Domain.Courses;
using HorsesForCourses.Core.Domain.Courses.InvalidationReasons;
using QuickPulse.Explains;

namespace HorsesForCourses.Tests.Documentation.Courses.A_CreateCourse;

public class B_CreateCourseDomain
{
    [Fact]
    public void CreateCourse_WithValidData_ShouldSucceed()
    {
        var course = TheCannonical.Course();
        Assert.Equal(TheCannonical.CourseName, course.Name);
        Assert.Equal(TheCannonical.CourseStart, course.StartDate);
        Assert.Equal(TheCannonical.CourseEnd, course.EndDate);
    }

    [Fact]
    public void CreateCourse_WithValidData_keeps_default_id()
    {
        var course = TheCannonical.Course();
        Assert.Equal(default, course.Id.Value);
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
