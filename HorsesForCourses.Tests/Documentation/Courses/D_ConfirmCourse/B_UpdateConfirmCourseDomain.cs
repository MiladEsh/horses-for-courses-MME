using HorsesForCourses.Core.Domain.Courses;
using HorsesForCourses.Core.Domain.Courses.InvalidationReasons;


namespace HorsesForCourses.Tests.Documentation.Courses.D_ConfirmCourse;

public class B_UpdateConfirmCourseDomain : CourseDomainTests
{
    protected override Course ManipulateEntity(Course entity)
        => entity.UpdateTimeSlots(TheCannonical.TimeSlotsFullDayMonday());

    [Fact]
    public void UpdateConfirmCourse_WithValidData_ShouldSucceed()
    {
        Entity.Confirm();
        Assert.True(Entity.IsConfirmed);
    }

    [Fact]
    public void UpdateConfirmCourse_Twice_Throws()
    {
        Entity.Confirm();
        Assert.Throws<CourseAlreadyComfirmed>(() => Entity.Confirm());
    }

    [Fact]
    public void UpdateConfirmCourse_Without_TimeSlots_Throws()
    {
        Entity.UpdateTimeSlots([]);
        Assert.Throws<AtLeastOneTimeSlotRequired>(() => Entity.Confirm());
    }
}
