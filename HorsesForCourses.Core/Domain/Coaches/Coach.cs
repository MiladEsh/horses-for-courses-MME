using HorsesForCourses.Core.Abstractions;
using HorsesForCourses.Core.Domain.Coaches.InvalidationReasons;

namespace HorsesForCourses.Core.Domain.Coaches;

public class Coach : DomainEntity<Coach>
{
    public string Name { get; init; }
    public string Email { get; init; }

    public HashSet<Skill> Skills { get; init; } = new();

    private readonly List<Course> assignedCourses = new();
    public IReadOnlyCollection<Course> AssignedCourses => assignedCourses.AsReadOnly();

    public Coach(string name, string email) //: base(Id<Coach>.New())
    {
        if (string.IsNullOrWhiteSpace(name))
            throw new CoachNameCanNotBeEmpty();
        if (string.IsNullOrWhiteSpace(email))
            throw new CoachEmailCanNotBeEmpty();

        Name = name;
        Email = email;
    }

    public void AddSkill(Skill skill)
    {
        if (Skills.Contains(skill))
            throw new CoachAlreadyHasSkill(skill.Value);
        Skills.Add(skill);
    }

    public void RemoveSkill(Skill skill)
    {
        Skills.Remove(skill);
    }

    public bool IsSuitableFor(Course course)
    {
        return course.RequiredSkills.All(Skills.Contains);
    }

    public void AssignCourse(Course course)
    {
        assignedCourses.Add(course);
    }

    public void UnassignCourse(Course course)
    {
        assignedCourses.Remove(course);
    }

    public bool IsAvailableFor(Course course)
    {
        foreach (var assigned in assignedCourses)
        {
            if (DatesOverlap(course, assigned))
            {
                foreach (var newSlot in course.TimeSlots)
                {
                    foreach (var existingSlot in assigned.TimeSlots)
                    {
                        if (newSlot.Day == existingSlot.Day &&
                            newSlot.Start < existingSlot.End &&
                            newSlot.End > existingSlot.Start
                            )
                        {
                            return false;
                        }
                    }
                }
            }
        }

        return true;
    }

    private bool DatesOverlap(Course a, Course b)
    {
        return a.StartDate <= b.EndDate && a.EndDate >= b.StartDate;
    }
}
