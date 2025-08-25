using HorsesForCourses.Core.Domain.Courses.InvalidationReasons;
using HorsesForCourses.Core.Domain.Skills;
using HorsesForCourses.Tests.Documentation.Coaches;


namespace HorsesForCourses.Tests.Documentation.Courses.B_UpdateConfirmCourse;

public class B_UpdateConfirmCourseDomain : CourseDomainTests
{

    [Fact]
    public void UpdateConfirmCourse_WithValidData_ShouldSucceed()
    {
        Entity.Confirm();
        Assert.True(Entity.IsConfirmed);
    }

    [Fact]
    public void UpdateConfirmCourse_WithInValidSkill_Throws()
    {
        Entity.Confirm();
        Assert.Throws<SkillValueCanNotBeEmpty>(() => Entity.Confirm());
    }
}
