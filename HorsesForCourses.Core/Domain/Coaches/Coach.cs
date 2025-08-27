using HorsesForCourses.Core.Abstractions;
using HorsesForCourses.Core.Domain.Coaches.InvalidationReasons;
using HorsesForCourses.Core.Domain.Courses;
using HorsesForCourses.Core.Domain.Skills;
using HorsesForCourses.Core.Extensions;

namespace HorsesForCourses.Core.Domain.Coaches;

public class Coach : DomainEntity<Coach>
{
    public string Name { get; init; }
    public string Email { get; init; }

    public HashSet<Skill> Skills { get; init; } = new();

    private readonly List<Course> assignedCourses = new();
    public IReadOnlyCollection<Course> AssignedCourses => assignedCourses.AsReadOnly();

    public Coach(string name, string email)
    {
        if (string.IsNullOrWhiteSpace(name)) // Make a value object, check string length
            throw new CoachNameCanNotBeEmpty();
        if (string.IsNullOrWhiteSpace(email))
            throw new CoachEmailCanNotBeEmpty();
        Name = name;
        Email = email;
    }

    private static bool NotAllowedWhenThereAreDuplicateSkills(IEnumerable<string> skills)
        => skills.NoDuplicatesAllowed(a => new CoachAlreadyHasSkill(string.Join(",", a)));

    public virtual void UpdateSkills(IEnumerable<string> skills)
    {
        NotAllowedWhenThereAreDuplicateSkills(skills);
        Skills.Clear();
        skills.Select(Skill.From)
            .ToList()
            .ForEach(a => Skills.Add(a));
    }

    public bool IsSuitableFor(Course course)
        => course.RequiredSkills.All(Skills.Contains);

    public void AssignCourse(Course course)
    {
        assignedCourses.Add(course);
    }
}
