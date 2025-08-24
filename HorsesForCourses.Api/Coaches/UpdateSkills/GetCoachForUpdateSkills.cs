using HorsesForCourses.Core.Abstractions;
using HorsesForCourses.Core.Domain.Coaches;
using Microsoft.EntityFrameworkCore;

namespace HorsesForCourses.Api.Coaches.UpdateSkills;

public interface IGetCoachForUpdateSkills
{
    Task<Coach?> Load(int id);
}

public class GetCoachForUpdateSkills : IGetCoachForUpdateSkills
{
    private readonly DbContext dbContext;

    public GetCoachForUpdateSkills(DbContext dbContext)
    {
        this.dbContext = dbContext;
    }

    public async Task<Coach?> Load(int id)
    {
        return await dbContext.FindAsync<Coach>(Id<Coach>.From(id));
    }
}