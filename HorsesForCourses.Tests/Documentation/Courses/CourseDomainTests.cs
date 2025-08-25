using HorsesForCourses.Core.Domain.Courses;

namespace HorsesForCourses.Tests.Documentation.Coaches;

public class CourseDomainTests : DomainTests<Course>
{
    protected override Course CreateCannonicalEntity() => TheCannonical.Course();
}