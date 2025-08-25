using HorsesForCourses.Core.Domain.Coaches;

namespace HorsesForCourses.Tests.Tools.Coaches;

public class CoachSpy : Coach
{
    public CoachSpy() : base(TheCannonical.CoachName, TheCannonical.CoachEmail) { }
    public bool Called;
    public IEnumerable<string>? Seen;
    public override void UpdateSkills(IEnumerable<string> skills)
    {
        Called = true; Seen = skills;
    }
}