using HorsesForCourses.Api.Warehouse;
using HorsesForCourses.Core.Domain.Courses;
using HorsesForCourses.Tests.Tools;


namespace HorsesForCourses.Tests.Documentation.Courses.E_AssignCoach;

public class C_AssignCoachData : TheDatabaseTest
{
    private readonly Course course;

    public C_AssignCoachData()
    {
        course = TheCannonical.Course();
        AddToDb(course);
    }

    private void Act()
    {
        var context = GetDbContext();

        var entity = Reload(context);
        entity.UpdateTimeSlots(TheCannonical.TimeSlotsFullDayMonday());
        entity.Confirm();
        context.SaveChanges();
    }

    private Course Reload() => Reload(GetDbContext());
    private Course Reload(AppDbContext dbContext) => dbContext.Courses.Single(a => a.Id == course.Id);

    [Fact(Skip = "Todo")]
    public void Skills_can_be_updated()
    {
        Act();
        Assert.True(Reload().IsConfirmed);
    }
}