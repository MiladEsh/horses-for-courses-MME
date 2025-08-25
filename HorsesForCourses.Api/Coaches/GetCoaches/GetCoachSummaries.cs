using HorsesForCourses.Api.Warehouse;
using HorsesForCourses.Api.Warehouse.Paging;
using Microsoft.EntityFrameworkCore;

namespace HorsesForCourses.Api.Coaches;

public interface IGetTheCoachSummaries
{
    Task<PagedResult<CoachSummary>> All(PageRequest request);
}

public class GetCoachSummaries : IGetTheCoachSummaries
{
    private readonly AppDbContext dbContext;

    public GetCoachSummaries(AppDbContext dbContext)
    {
        this.dbContext = dbContext;
    }
    public async Task<PagedResult<CoachSummary>> All(PageRequest request)
    {
        return await dbContext.Coaches
            .AsNoTracking()
            .OrderBy(p => p.Name).ThenBy(p => p.Id)
            .Select(p => new CoachSummary(
                p.Id.Value,
                p.Name,
                p.Email,
                p.AssignedCourses.Count))
            .ToPagedResultAsync(request);
    }
}