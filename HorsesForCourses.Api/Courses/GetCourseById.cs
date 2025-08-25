using HorsesForCourses.Core.Abstractions;
using HorsesForCourses.Core.Domain.Courses;
using Microsoft.EntityFrameworkCore;

namespace HorsesForCourses.Api.Courses;

public interface IGetCourseById
{
    Task<Course?> Load(int id);
}

public class GetCourseById : IGetCourseById
{
    private readonly DbContext dbContext;

    public GetCourseById(DbContext dbContext)
    {
        this.dbContext = dbContext;
    }

    public async Task<Course?> Load(int id)
    {
        return await dbContext.FindAsync<Course>(Id<Course>.From(id));
    }
}