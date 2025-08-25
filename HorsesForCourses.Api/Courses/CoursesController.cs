using HorsesForCourses.Api.Coaches;
using HorsesForCourses.Api.Courses.CreateCourse;
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
    private readonly IGetCourseById getCourseById;
    private readonly IGetCoachById getCoachById;

    public CoursesController(
        IAmASuperVisor supervisor,
        IGetCourseById getCourseById,
        IGetCoachById getCoachById)
    {
        this.supervisor = supervisor;
        this.getCourseById = getCourseById;
        this.getCoachById = getCoachById;
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
        var course = await getCourseById.Load(id);
        if (course == null) return NotFound();
        course.UpdateRequiredSkills(skills);
        return NoContent();
    }

    [HttpPost("{id}/timeslots")]
    public async Task<IActionResult> UpdateTimeSlots(int id, IEnumerable<TimeSlotRequest> timeSlots)
    {
        var course = await getCourseById.Load(id);
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
        var course = await getCourseById.Load(id);
        if (course == null) return NotFound();
        course.Confirm();
        return NoContent();
    }


    [HttpPost("{id}/assign-coach")]
    public async Task<IActionResult> AssignCoach(int id, int coachId)
    {
        var course = await getCourseById.Load(id);
        var coach = await getCoachById.Load(coachId);
        if (course == null || coach == null) return NotFound();
        course.AssignCoach(coach);
        return NoContent();
    }
}