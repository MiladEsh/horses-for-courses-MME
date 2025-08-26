using HorsesForCourses.Core.Domain.Coaches;
using HorsesForCourses.Api.Warehouse;
using Microsoft.AspNetCore.Mvc;
using HorsesForCourses.Api.Coaches.RegisterCoach;
using HorsesForCourses.Api.Coaches.UpdateSkills;
using HorsesForCourses.Api.Warehouse.Paging;
using HorsesForCourses.Api.Coaches.GetCoaches;

namespace HorsesForCourses.Api.Coaches;

[ApiController]
[Route("coaches")]
public class CoachesController : ControllerBase
{
    private readonly IAmASuperVisor supervisor;
    private readonly IGetCoachById getCoachById;
    private readonly IGetTheCoachSummaries getTheCoachSummaries;
    private readonly IGetTheCoachDetail getTheCoachDetail;

    public CoachesController(
        IAmASuperVisor supervisor,
        IGetCoachById getCoachById,
        IGetTheCoachSummaries getTheCoachSummaries,
        IGetTheCoachDetail getTheCoachDetail)
    {
        this.supervisor = supervisor;
        this.getCoachById = getCoachById;
        this.getTheCoachSummaries = getTheCoachSummaries;
        this.getTheCoachDetail = getTheCoachDetail;
    }

    [HttpPost]
    public async Task<IActionResult> RegisterCoach(RegisterCoachRequest request)
    {
        var coach = new Coach(request.Name, request.Email);
        await supervisor.Enlist(coach);
        await supervisor.Ship();
        return Ok(coach.Id.Value);
    }

    [HttpPost("{id}/skills")]
    public async Task<IActionResult> UpdateSkills(int id, UpdateSkillsRequest request)
    {
        var coach = await getCoachById.Load(id);
        if (coach == null) return NotFound();
        coach.UpdateSkills(request.Skills);
        await supervisor.Ship();
        return NoContent();
    }

    [HttpGet]
    public async Task<IActionResult> GetCoaches(int page = 1, int pageSize = 25)
        => Ok(await getTheCoachSummaries.All(new PageRequest(page, pageSize)));

    [HttpGet("{id}")]
    public async Task<IActionResult> GetCoachDetail(int id)
        => Ok(await getTheCoachDetail.One(id));
}
