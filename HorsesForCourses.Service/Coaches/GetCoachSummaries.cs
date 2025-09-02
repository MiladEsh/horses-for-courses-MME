using HorsesForCourses.Api.Warehouse;
using HorsesForCourses.Service.Coaches.Models;
using Microsoft.EntityFrameworkCore;

namespace HorsesForCourses.Service.Coaches;

public class GetCoachSummaries : IGetTheCoachSummaries
{
    private readonly AppDbContext _db;

    public GetCoachSummaries(AppDbContext db)
    {
        _db = db;
    }

    public async Task<IReadOnlyList<CoachSummaryDto>> Execute()
    {
        var coaches = await _db.Coaches.ToListAsync();
        return coaches.Select(c => new CoachSummaryDto(c.Id, c.Name, c.Email)).ToList();
    }
}