using HorsesForCourses.Core.Domain;
using Microsoft.AspNetCore.Mvc;

namespace HorsesForCourses.WebApi.Controllers;

[ApiController]
[Route("coaches")]
public class CoachController : ControllerBase
{
    private readonly CoachRepository repo;

    public CoachController(CoachRepository repo)
    {
        this.repo = repo;
    }


    public record RegisterCoachRequest(string Name, string Email);

    [HttpPost]
    public IActionResult RegisterCoach(RegisterCoachRequest request)
    {
        var coach = new Coach(request.Name, request.Email);
        repo.Save(coach);
        return Ok();
    }


    public record UpdateSkillsRequest(List<string> Skills);

    [HttpPost("{id}/skills")]
    public IActionResult UpdateSkills(Guid id, UpdateSkillsRequest request)
    {
        var coach = repo.Get(id);
        if (coach == null) return NotFound();
        //coach.ReplaceSkills(request.Skills);
        return Ok();
    }
}
