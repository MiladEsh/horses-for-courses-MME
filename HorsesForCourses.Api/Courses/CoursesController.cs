using HorsesForCourses.Api.Coursees.UpdateSkills;
using HorsesForCourses.Api.Courses.CreateCoach;
using HorsesForCourses.Api.Warehouse;
using HorsesForCourses.Core.Domain.Courses;
using Microsoft.AspNetCore.Mvc;

namespace HorsesForCourses.Api.Courses;

[ApiController]
[Route("courses")]
public class CoursesController : ControllerBase
{
    private readonly IAmASuperVisor supervisor;
    private readonly IGetCourseForUpdateSkills getCourseForSkills;

    public CoursesController(IAmASuperVisor supervisor, IGetCourseForUpdateSkills getCourseForSkills)
    {
        this.supervisor = supervisor;
        this.getCourseForSkills = getCourseForSkills;
    }

    [HttpPost]
    public async Task<IActionResult> CreateCourse(CreateCourseRequest request)
    {
        var course = new Course(request.Name, request.StartDate, request.EndDate);
        await supervisor.Enlist(course);
        await supervisor.Ship();
        return Ok(course.Id.Value);
    }

    [HttpPost("{id}/skills")]
    public async Task<ActionResult> UpdateRequiredSkills(int id, IEnumerable<string> skills)
    {
        var course = await getCourseForSkills.Load(id);
        if (course == null) return NotFound();
        course.UpdateRequiredSkills(skills);
        return NoContent();
    }

    // public record TimeslotDto(string Day, TimeOnly Start, TimeOnly End);
    // public record UpdateTimeslotsRequest(List<TimeslotDto> Timeslots);

    // [HttpPost("{id}/timeslots")]
    // public IActionResult UpdateTimeslots(Guid id, UpdateTimeslotsRequest request)
    // {
    //     var course = repo.Get(id);
    //     if (course == null) return NotFound();
    //     //course.ReplaceTimeslots(request.Timeslots);
    //     return Ok();
    // }


    // [HttpPost("{id}/confirm")]
    // public IActionResult Confirm(Guid id)
    // {
    //     var course = repo.Get(id);
    //     if (course == null) return NotFound();
    //     //course.Confirm();
    //     return Ok();
    // }


    // public record AssignCoachRequest(Guid CoachId);

    // [HttpPost("{id}/assign-coach")]
    // public IActionResult AssignCoach(Guid id, AssignCoachRequest request)
    // {
    //     var course = repo.Get(id);
    //     var coach = coaches.Get(request.CoachId);
    //     if (course == null || coach == null) return NotFound();
    //     //course.AssignCoach(coach);
    //     return Ok();
    // }
}