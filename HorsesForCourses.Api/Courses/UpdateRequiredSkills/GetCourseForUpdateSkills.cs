using HorsesForCourses.Core.Abstractions;
using HorsesForCourses.Core.Domain.Courses;
using Microsoft.EntityFrameworkCore;

namespace HorsesForCourses.Api.Coursees.UpdateSkills;

public interface IGetCourseForUpdateSkills
{
    Task<Course?> Load(int id);
}

public class GetCourseForUpdateSkills : IGetCourseForUpdateSkills
{
    private readonly DbContext dbContext;

    public GetCourseForUpdateSkills(DbContext dbContext)
    {
        this.dbContext = dbContext;
    }

    public async Task<Course?> Load(int id)
    {
        return await dbContext.FindAsync<Course>(Id<Course>.From(id));
    }
}