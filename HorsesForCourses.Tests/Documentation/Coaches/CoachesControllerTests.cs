using HorsesForCourses.Api.Coaches;
using HorsesForCourses.Api.Coaches.UpdateSkills;
using HorsesForCourses.Api.Warehouse;
using Moq;

namespace HorsesForCourses.Tests.Documentation.Coaches;


public abstract class CoachesControllerTests
{
    protected readonly CoachesController controller;
    protected readonly Mock<IAmASuperVisor> supervisor;
    protected readonly Mock<IGetCoachForUpdateSkills> query;
    protected readonly CoachSpy spy;

    public CoachesControllerTests()
    {
        spy = new();
        query = new Mock<IGetCoachForUpdateSkills>();
        query.Setup(a => a.Load(It.IsAny<int>())).ReturnsAsync(spy);
        supervisor = new Mock<IAmASuperVisor>();
        controller = new CoachesController(supervisor.Object, query.Object);
    }
}
