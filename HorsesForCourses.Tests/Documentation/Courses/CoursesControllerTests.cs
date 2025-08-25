using HorsesForCourses.Api.Courses;
using HorsesForCourses.Api.Warehouse;
using Moq;

namespace HorsesForCourses.Tests.Documentation.Courses;

public abstract class CoursesControllerTests
{
    protected readonly CoursesController controller;
    protected readonly Mock<IAmASuperVisor> supervisor;
    // protected readonly Mock<IGetCoachForUpdateSkills> query;
    // protected readonly CoachSpy spy;

    public CoursesControllerTests()
    {
        // spy = new();
        // query = new Mock<IGetCoachForUpdateSkills>();
        // query.Setup(a => a.Load(It.IsAny<int>())).ReturnsAsync(spy);
        supervisor = new Mock<IAmASuperVisor>();
        controller = new CoursesController(supervisor.Object);
    }
}