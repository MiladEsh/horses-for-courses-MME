using HorsesForCourses.Api.Courses.UpdateTimeSlots;
using Microsoft.AspNetCore.Mvc;
using Moq;

namespace HorsesForCourses.Tests.Documentation.Courses.C_UpdateTimeSlots;


public class A_UpdateTimeSlotsApi : CoursesControllerTests
{
    private readonly IEnumerable<TimeSlotRequest> request =
        TheCannonical.TimeSlotsRequestFullDayMonday();

    [Fact]
    public async Task UpdateTimeSlots_uses_the_query_object()
    {
        var response = await controller.UpdateTimeSlots(42, request);
        courseQuery.Verify(a => a.Load(42));
    }

    [Fact]
    public async Task UpdateTimeSlots_calls_domain_entity()
    {
        await controller.UpdateTimeSlots(42, request);
        Assert.True(spy.TimeSlotsCalled);
        Assert.Equal(TheCannonical.TimeSlotsFullDayMonday(), spy.TimeSlotsSeen);
    }

    [Fact]
    public async Task UpdateTimeSlots_does_not_call_supervisor_ship()
    {
        await controller.UpdateTimeSlots(42, request);
        supervisor.Verify(a => a.Ship(), Times.Never);
    }

    [Fact]
    public async Task UpdateTimeSlots_Returns_NoContent()
    {
        var response = await controller.UpdateTimeSlots(42, request);
        Assert.IsType<NoContentResult>(response);
    }
}