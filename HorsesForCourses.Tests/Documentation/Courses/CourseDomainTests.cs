using HorsesForCourses.Core.Domain.Courses;

namespace HorsesForCourses.Tests.Documentation.Courses;

public class CourseDomainTests : DomainTests<Course>
{
    protected override Course CreateCannonicalEntity() => TheCannonical.Course();
}