using HorsesForCourses.Api.Coaches;
using HorsesForCourses.Api.Courses;
using HorsesForCourses.Api.Warehouse;
using HorsesForCourses.Core.Domain.Courses;
using Moq;

namespace HorsesForCourses.Tests.Tools.Courses;

public abstract class CoursesControllerTests
{
    protected readonly CoursesController controller;
    protected readonly Mock<IAmASuperVisor> supervisor;
    protected readonly Mock<IGetCourseById> courseQuery;
    protected readonly Mock<IGetCoachById> coachQuery;
    protected readonly CourseSpy spy;

    public CoursesControllerTests()
    {
        spy = new();
        ManipulateEntity(spy);

        courseQuery = new Mock<IGetCourseById>();
        courseQuery.Setup(a => a.Load(It.IsAny<int>())).ReturnsAsync(spy);

        coachQuery = new Mock<IGetCoachById>();
        coachQuery.Setup(a => a.Load(It.IsAny<int>())).ReturnsAsync(TheCannonical.Coach());

        supervisor = new Mock<IAmASuperVisor>();
        controller = new CoursesController(supervisor.Object, courseQuery.Object, coachQuery.Object);
    }

    protected virtual void ManipulateEntity(Course entity) { }
}