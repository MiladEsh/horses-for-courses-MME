using HorsesForCourses.Api.Coursees.UpdateSkills;
using HorsesForCourses.Api.Courses;
using HorsesForCourses.Api.Warehouse;
using HorsesForCourses.Core.Domain.Coaches;
using HorsesForCourses.Core.Domain.Courses;
using HorsesForCourses.Tests.Documentation.Coaches;
using Moq;

namespace HorsesForCourses.Tests.Documentation.Courses;

public abstract class CoursesControllerTests
{
    protected readonly CoursesController controller;
    protected readonly Mock<IAmASuperVisor> supervisor;
    protected readonly Mock<IGetCourseForUpdateSkills> query;
    protected readonly CourseSpy spy;

    public CoursesControllerTests()
    {
        spy = new();
        ManipulateEntity(spy);
        query = new Mock<IGetCourseForUpdateSkills>();
        query.Setup(a => a.Load(It.IsAny<int>())).ReturnsAsync(spy);
        supervisor = new Mock<IAmASuperVisor>();
        controller = new CoursesController(supervisor.Object, query.Object);
    }

    protected virtual void ManipulateEntity(Course entity) { }
}