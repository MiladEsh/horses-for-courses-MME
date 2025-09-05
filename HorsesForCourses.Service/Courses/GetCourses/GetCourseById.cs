using HorsesForCourses.Service.Warehouse;
using HorsesForCourses.Core.Abstractions;
using HorsesForCourses.Core.Domain.Courses;

namespace HorsesForCourses.Service.Courses.GetCourses;

public interface IGetCourseById
{
    Task<Course?> Load(int id);
}

public class GetCourseById : IGetCourseById
{
    private readonly AppDbContext dbContext;

    public GetCourseById(AppDbContext dbContext)
    {
        this.dbContext = dbContext;
    }

    public async Task<Course?> Load(int id)
    {
        return await dbContext.FindAsync<Course>(Id<Course>.From(id));
    }
}