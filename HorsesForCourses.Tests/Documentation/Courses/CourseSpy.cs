using HorsesForCourses.Core.Domain.Courses;

namespace HorsesForCourses.Tests.Documentation.Courses;

public class CourseSpy : Course
{
    public CourseSpy() : base(TheCannonical.CourseName, TheCannonical.CourseStart, TheCannonical.CourseEnd) { }
    public bool Called;
    public IEnumerable<string>? Seen;
    public override void UpdateRequiredSkills(IEnumerable<string> skills)
    {
        Called = true; Seen = skills;
    }
}