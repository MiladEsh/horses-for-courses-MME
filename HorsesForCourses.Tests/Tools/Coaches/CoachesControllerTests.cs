using HorsesForCourses.Api.Coaches;
using HorsesForCourses.Api.Coaches.GetCoaches;
using HorsesForCourses.Api.Warehouse;
using HorsesForCourses.Api.Warehouse.Paging;
using Moq;

namespace HorsesForCourses.Tests.Tools.Coaches;

public abstract class CoachesControllerTests
{
    protected readonly CoachesController controller;
    protected readonly Mock<IAmASuperVisor> supervisor;
    protected readonly Mock<IGetCoachById> coachQuery;
    protected readonly Mock<IGetTheCoachSummaries> getCoachSummaries;
    protected readonly Mock<IGetTheCoachDetail> getCoachDetail;
    protected readonly CoachSpy spy;

    public CoachesControllerTests()
    {
        getCoachDetail = new Mock<IGetTheCoachDetail>();
        getCoachDetail.Setup(a => a.One(It.IsAny<int>())).ReturnsAsync(new CoachDetail());
        getCoachSummaries = new Mock<IGetTheCoachSummaries>();
        getCoachSummaries.Setup(a => a.All(It.IsAny<PageRequest>())).ReturnsAsync(new PagedResult<CoachSummary>([], 0, 1, 15));
        spy = new();
        coachQuery = new Mock<IGetCoachById>();
        coachQuery.Setup(a => a.Load(It.IsAny<int>())).ReturnsAsync(spy);
        supervisor = new Mock<IAmASuperVisor>();
        controller = new CoachesController(
            supervisor.Object,
            coachQuery.Object,
            getCoachSummaries.Object,
            getCoachDetail.Object);
    }
}
