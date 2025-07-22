using HorsesForCourses.Core.Domain;
using Microsoft.AspNetCore.Mvc;

namespace HorsesForCourses.WebApi.Controllers;

[ApiController]
[Route("courses")]
public class CourseController : ControllerBase
{
    private readonly CourseRepository repo;
    private readonly CoachRepository coaches;

    public CourseController(CourseRepository repo, CoachRepository coaches)
    {
        this.repo = repo;
        this.coaches = coaches;
    }


    public record CreateCourseRequest(string Name, DateOnly StartDate, DateOnly EndDate);

    [HttpPost]
    public IActionResult CreateCourse(CreateCourseRequest request)
    {
        var course = new Course(request.Name, request.StartDate, request.EndDate);
        repo.Save(course);
        return Ok();
    }


    public record UpdateSkillsRequest(List<string> Skills);

    [HttpPost("{id}/skills")]
    public IActionResult UpdateRequiredSkills(Guid id, UpdateSkillsRequest request)
    {
        var course = repo.Get(id);
        if (course == null) return NotFound();
        //course.ReplaceRequiredSkills(request.Skills);
        return Ok();
    }

    public record TimeslotDto(string Day, TimeOnly Start, TimeOnly End);
    public record UpdateTimeslotsRequest(List<TimeslotDto> Timeslots);

    [HttpPost("{id}/timeslots")]
    public IActionResult UpdateTimeslots(Guid id, UpdateTimeslotsRequest request)
    {
        var course = repo.Get(id);
        if (course == null) return NotFound();
        //course.ReplaceTimeslots(request.Timeslots);
        return Ok();
    }


    [HttpPost("{id}/confirm")]
    public IActionResult Confirm(Guid id)
    {
        var course = repo.Get(id);
        if (course == null) return NotFound();
        //course.Confirm();
        return Ok();
    }


    public record AssignCoachRequest(Guid CoachId);

    [HttpPost("{id}/assign-coach")]
    public IActionResult AssignCoach(Guid id, AssignCoachRequest request)
    {
        var course = repo.Get(id);
        var coach = coaches.Get(request.CoachId);
        if (course == null || coach == null) return NotFound();
        //course.AssignCoach(coach);
        return Ok();
    }
}