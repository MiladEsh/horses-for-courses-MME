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
    private IGetCoachForUpdateSkills updateSkillsQuery;


    public CoachesController(IAmASuperVisor supervisor, IGetCoachForUpdateSkills updateSkillsQuery)
    {
        this.supervisor = supervisor;
        this.updateSkillsQuery = updateSkillsQuery;
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
        var coach = await updateSkillsQuery.Load(id);
        if (coach == null) return NotFound();
        coach.UpdateSkills(request.Skills);
        return NoContent();
    }
}
