using HorsesForCourses.Core.Domain.Coaches;
using HorsesForCourses.Api.Warehouse;
using Microsoft.AspNetCore.Mvc;
using HorsesForCourses.Api.Coaches.RegisterCoach;
using HorsesForCourses.Api.Coaches.UpdateSkills;

namespace HorsesForCourses.Api.Coaches;

[ApiController]
[Route("coaches")]
public class CoachesController : ControllerBase
{
    private readonly IAmASuperVisor supervisor;
    private readonly IGetCoachById getCoachById;
    private readonly IGetTheCoachSummaries getTheCoachSummaries;

    public CoachesController(
        IAmASuperVisor supervisor,
        IGetCoachById getCoachById,
        IGetTheCoachSummaries getTheCoachSummaries)
    {
        this.supervisor = supervisor;
        this.getCoachById = getCoachById;
        this.getTheCoachSummaries = getTheCoachSummaries;
    }

    [HttpPost]
    public async Task<IActionResult> RegisterCoach(RegisterCoachRequest request)
    {
        var coach = new Coach(request.Name, request.Email);
        await supervisor.Enlist(coach);
        await supervisor.Ship();
        return Ok(coach.Id.Value);
    }

    [HttpPost]
    public async Task<IActionResult> UpdateSkills(int id, UpdateSkillsRequest request)
    {
        var coach = await getCoachById.Load(id);
        if (coach == null) return NotFound();
        coach.UpdateSkills(request.Skills);
        return NoContent();
    }

    [HttpGet]
    public async Task<IActionResult> GetCoaches(int page = 1, int pageSize = 25)
        => Ok(await getTheCoachSummaries.All(new PageRequest(page, pageSize)));
}
