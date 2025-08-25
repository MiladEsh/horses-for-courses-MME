using HorsesForCourses.Api.Warehouse;
using HorsesForCourses.Core.Abstractions;
using HorsesForCourses.Core.Domain.Coaches;
using Microsoft.EntityFrameworkCore;

namespace HorsesForCourses.Api.Coaches.GetCoaches;

public interface IGetTheCoachDetail
{
    Task<CoachDetail?> One(int id);
}

public class GetCoachDetail : IGetTheCoachDetail
{
    private readonly AppDbContext dbContext;

    public GetCoachDetail(AppDbContext dbContext)
    {
        this.dbContext = dbContext;
    }

    public async Task<CoachDetail?> One(int id)
    {
        var coachId = Id<Coach>.From(id);
        return await dbContext.Coaches
            .AsNoTracking()
            .Where(a => a.Id == coachId)
            .Select(a => new CoachDetail
            {
                Id = a.Id.Value,
                Name = a.Name,
                Email = a.Email,
                Skills = a.Skills.Select(a => a.Value).ToList(),
                Courses = a.AssignedCourses.Select(
                    b => new CoachDetail.CourseInfo(b.Id.Value, b.Name)).ToList()
            }).SingleOrDefaultAsync();
    }
}