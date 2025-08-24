using HorsesForCourses.Core.Domain.Coaches;

namespace HorsesForCourses.Api.Warehouse;

public interface IAmASuperVisor
{
    Task Enlist(Coach coach);
}

public class DataSupervisor : IAmASuperVisor
{
    private readonly AppDBContext dbContext;

    public DataSupervisor(AppDBContext dbContext)
    {
        this.dbContext = dbContext;
    }

    public async Task Enlist(Coach coach)
    {
        await dbContext.AddAsync(coach);
    }
}
