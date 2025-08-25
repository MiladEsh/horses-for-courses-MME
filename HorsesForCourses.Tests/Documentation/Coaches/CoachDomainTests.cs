using HorsesForCourses.Core.Domain.Coaches;

namespace HorsesForCourses.Tests.Documentation.Coaches;

public class CoachDomainTests : DomainTests<Coach>
{
    protected override Coach CreateCannonicalEntity() => TheCannonical.Coach();
}