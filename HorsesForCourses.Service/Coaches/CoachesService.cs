using HorsesForCourses.Service.Coaches.GetCoachDetail;
using HorsesForCourses.Service.Coaches.GetCoaches;
using HorsesForCourses.Core.Domain.Coaches;


namespace HorsesForCourses.Service.Coaches;

public class CoachesService
{

    public CoachesService(
        IGetCoachSummaries getCoaches,
        IGetTheCoachDetail getCoachDetail,
        CoachesRepository repository)
    {
        GetCoaches = getCoaches;
        GetCoachDetail = getCoachDetail;
        this.repository = repository;
    }

    public CoachesService()
    {
    }

    public IGetCoachSummaries? GetCoaches { get; }
    public IGetTheCoachDetail? GetCoachDetail { get; }

    private CoachesRepository? repository;

    public async Task<int> RegisterCoach(string name, string email)
    {
        var coach = new Coach(name, email);
        if (repository == null)
        {
            throw new InvalidOperationException("Repository is not initialized.");
        }
        await repository.Supervisor.Enlist(coach);
        await repository.Supervisor.Ship();
        return coach.Id.Value;
    }
}