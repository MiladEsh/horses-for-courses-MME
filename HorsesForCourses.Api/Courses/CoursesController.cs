using HorsesForCourses.Api.Coursees.UpdateSkills;
using HorsesForCourses.Api.Courses.CreateCoach;
using HorsesForCourses.Api.Courses.UpdateTimeSlots;
using HorsesForCourses.Api.Warehouse;
using HorsesForCourses.Core.Domain;
using HorsesForCourses.Core.Domain.Courses;
using Microsoft.AspNetCore.Mvc;

namespace HorsesForCourses.Api.Courses;

[ApiController]
[Route("courses")]
public partial class CoursesController : ControllerBase
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
    public async Task<IActionResult> UpdateRequiredSkills(int id, IEnumerable<string> skills)
    {
        var course = await getCourseForSkills.Load(id);
        if (course == null) return NotFound();
        course.UpdateRequiredSkills(skills);
        return NoContent();
    }

    [HttpPost("{id}/timeslots")]
    public async Task<IActionResult> UpdateTimeSlots(int id, IEnumerable<TimeSlotDto> timeSlots)
    {
        var course = await getCourseForSkills.Load(id);
        if (course == null) return NotFound();
        course.UpdateTimeSlots(
            timeSlots.Select(
                a => TimeSlot.From(
                    a.Day,
                    OfficeHour.From(a.Start),
                    OfficeHour.From(a.End))).ToList());
        return NoContent();
    }

    [HttpPost("{id}/confirm")]
    public async Task<IActionResult> ConfirmCourse(int id)
    {
        var course = await getCourseForSkills.Load(id);
        if (course == null) return NotFound();
        course.Confirm();
        return NoContent();
    }



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