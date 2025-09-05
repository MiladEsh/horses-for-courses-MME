using HorsesForCourses.Service.Coaches.GetCoachDetail;
using HorsesForCourses.Service.Coaches.GetCoaches;
using HorsesForCourses.Service.Warehouse;

namespace HorsesForCourses.Api.Coaches;

public class CoachesRepository
{
    public CoachesRepository(
        IAmASuperVisor supervisor,
        IGetCoachById getCoachById,
        IGetCoachSummaries getTheCoachSummaries,
        IGetTheCoachDetail getTheCoachDetail)
    {
        Supervisor = supervisor;
        GetCoachById = getCoachById;
        GetTheCoachSummaries = getTheCoachSummaries;
        GetTheCoachDetail = getTheCoachDetail;
    }

    public IAmASuperVisor Supervisor { get; }
    public IGetCoachById GetCoachById { get; }
    public IGetCoachSummaries GetTheCoachSummaries { get; }
    public IGetTheCoachDetail GetTheCoachDetail { get; }
}