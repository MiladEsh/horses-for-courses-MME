using HorsesForCourses.Api.Warehouse;
using HorsesForCourses.Core.Domain.Courses;
using HorsesForCourses.Core.Domain.Skills;


namespace HorsesForCourses.Tests.Documentation.Courses.B_UpdateRequiredSkills;

public class C_UpdateRequiredSkillsData : TheDatabaseTest
{
    private readonly Course course;

    public C_UpdateRequiredSkillsData()
    {
        course = TheCannonical.Course();
        AddToDb(course);
    }

    private void Act()
    {
        var context = GetDbContext();
        Reload(context).UpdateRequiredSkills(["one", "two"]);
        context.SaveChanges();
    }

    private Course Reload() => Reload(GetDbContext());
    private Course Reload(AppDbContext dbContext) => dbContext.Courses.Single(a => a.Id == course.Id);

    [Fact]
    public void Skills_can_be_updated()
    {
        Act();
        Assert.Equal([Skill.From("one"), Skill.From("two")], Reload().RequiredSkills);
    }
}