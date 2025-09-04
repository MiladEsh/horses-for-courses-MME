using HorsesForCourses.Service.Warehouse;
using Microsoft.EntityFrameworkCore;
using HorsesForCourses.Service.Warehouse.Paging;

namespace HorsesForCourses.Service.Coaches.GetCoaches;

public interface IGetCoachSummaries
{
    Task<PagedResult<CoachSummary>> All(PageRequest request);
}

public class GetCoachSummaries(AppDbContext dbContext) : IGetCoachSummaries
{
    private readonly AppDbContext dbContext = dbContext;

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