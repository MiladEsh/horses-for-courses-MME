using HorsesForCourses.Core.Abstractions;
using HorsesForCourses.Core.Domain.Coaches;
using Microsoft.EntityFrameworkCore;

namespace HorsesForCourses.Api.Coaches;

public interface IGetCoachById
{
    Task<Coach?> Load(int id);
}

public class GetCoachById : IGetCoachById
{
    private readonly DbContext dbContext;

    public GetCoachById(DbContext dbContext)
    {
        this.dbContext = dbContext;
    }

    public async Task<Coach?> Load(int id)
    {
        return await dbContext.FindAsync<Coach>(Id<Coach>.From(id));
    }
}