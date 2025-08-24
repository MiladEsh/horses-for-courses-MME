using HorsesForCourses.Core.Domain.Coaches;
using HorsesForCourses.Api.Warehouse;
using Microsoft.AspNetCore.Mvc;
using HorsesForCourses.Core.Domain;

namespace HorsesForCourses.Api.Coaches;

[ApiController]
[Route("coaches")]
public class CoachesController : ControllerBase
{
    private readonly IAmASuperVisor supervisor;

    public CoachesController(IAmASuperVisor supervisor)
    {
        this.supervisor = supervisor;
    }

    public record RegisterCoachRequest(string Name, string Email);
    public class RegisterCoachRequestCanNotBeNull : ApiException{};
    [HttpPost]
    public async Task<IActionResult> RegisterCoach(RegisterCoachRequest request)
    {
        if (request == null) throw new RegisterCoachRequestCanNotBeNull();
        var coach = new Coach(request.Name, request.Email);
        await supervisor.Enlist(coach);
        return Ok(coach.Id.Value);
    }
}
