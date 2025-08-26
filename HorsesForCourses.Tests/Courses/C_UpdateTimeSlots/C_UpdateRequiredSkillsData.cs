using HorsesForCourses.Tests.Tools;
using HorsesForCourses.Tests.Tools.Courses;

namespace HorsesForCourses.Tests.Courses.C_UpdateTimeSlots;

public class C_UpdateTimeSlotsData : CourseDatabaseTests
{
    private void Act()
    {
        var context = GetDbContext();
        Reload(context).UpdateTimeSlots(TheCannonical.TimeSlotsFullDayMonday());
        context.SaveChanges();
    }

    [Fact]
    public void TimeSlots_can_be_updated()
    {
        Act();
        Assert.Equal(TheCannonical.TimeSlotsFullDayMonday(), Reload().TimeSlots);
    }
}