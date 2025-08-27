using HorsesForCourses.Api.Coaches;
using HorsesForCourses.Api.Courses.AssignCoach;
using HorsesForCourses.Api.Courses.CreateCourse;
using HorsesForCourses.Api.Courses.GetCourseDetail;
using HorsesForCourses.Api.Courses.GetCourses;
using HorsesForCourses.Api.Courses.UpdateTimeSlots;
using HorsesForCourses.Api.Warehouse;
using HorsesForCourses.Api.Warehouse.Paging;
using HorsesForCourses.Core.Domain;
using HorsesForCourses.Core.Domain.Courses;
using HorsesForCourses.Core.Domain.Courses.OfficeHours;
using HorsesForCourses.Core.Domain.Courses.TimeSlots;
using Microsoft.AspNetCore.Mvc;

namespace HorsesForCourses.Api.Courses;

[ApiController]
[Route("courses")]
public partial class CoursesController : ControllerBase
{
    private readonly IAmASuperVisor supervisor;
    private readonly IGetCourseById getCourseById;
    private readonly IGetCoachById getCoachById;
    private readonly IGetTheCourseSummaries getCourseSummaries;
    private readonly IGetTheCourseDetail getCourseDetail;

    public CoursesController(
        IAmASuperVisor supervisor,
        IGetCourseById getCourseById,
        IGetCoachById getCoachById,
        IGetTheCourseSummaries getCourseSummaries,
        IGetTheCourseDetail getCourseDetail)
    {
        this.supervisor = supervisor;
        this.getCourseById = getCourseById;
        this.getCoachById = getCoachById;
        this.getCourseSummaries = getCourseSummaries;
        this.getCourseDetail = getCourseDetail;
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
        await supervisor.Ship();
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
        await supervisor.Ship();
        return NoContent();
    }

    [HttpPost("{id}/confirm")]
    public async Task<IActionResult> ConfirmCourse(int id)
    {
        var course = await getCourseById.Load(id);
        if (course == null) return NotFound();
        course.Confirm();
        await supervisor.Ship();
        return NoContent();
    }


    [HttpPost("{id}/assign-coach")]
    public async Task<IActionResult> AssignCoach(int id, AssignCoachRequest request)
    {
        var course = await getCourseById.Load(id);
        var coach = await getCoachById.Load(request.CoachId);
        if (course == null || coach == null) return NotFound();
        course.AssignCoach(coach);
        await supervisor.Ship();
        return NoContent();
    }

    [HttpGet]
    public async Task<IActionResult> GetCourses(int page = 1, int pageSize = 25)
        => Ok(await getCourseSummaries.All(new PageRequest(page, pageSize)));

    [HttpGet("{id}")]
    public async Task<IActionResult> GetCourseDetail(int id)
    {
        var courseDetail = await getCourseDetail.One(id);
        if (courseDetail == null) return NotFound();
        return Ok(courseDetail);
    }
}