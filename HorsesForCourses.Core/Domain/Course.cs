using HorsesForCourses.Core.Abstractions;
using HorsesForCourses.Core.Domain.Coaches;

namespace HorsesForCourses.Core.Domain;

public class Course : DomainEntity<Course>
{
    public string Name { get; private set; }
    public DateOnly StartDate { get; private set; }
    public DateOnly EndDate { get; private set; }
    public List<TimeSlot> TimeSlots { get; private set; } = new();
    public List<Skill> RequiredSkills { get; private set; } = new();
    public bool IsConfirmed { get; private set; }

    public Coach? AssignedCoach { get; private set; }

    public Course(string name, DateOnly startDate, DateOnly endDate) : base(Id<Course>.New())
    {
        if (string.IsNullOrWhiteSpace(name))
            throw new ArgumentException("Course name cannot be empty.");

        if (startDate > endDate)
            throw new ArgumentException("Start date must be before end date.");

        Name = name;
        StartDate = startDate;
        EndDate = endDate;
    }

    public void AddRequiredSkill(Skill skill)
    {
        if (IsConfirmed)
            throw new InvalidOperationException("Cannot modify required skills after course is confirmed.");

        if (!RequiredSkills.Contains(skill))
        {
            RequiredSkills.Add(skill);
        }
    }

    public void RemoveRequiredSkill(Skill skill)
    {
        if (IsConfirmed)
            throw new InvalidOperationException("Cannot modify required skills after course is confirmed.");

        RequiredSkills.Remove(skill);
    }

    public void AddTimeSlot(TimeSlot slot)
    {
        if (IsConfirmed)
            throw new InvalidOperationException("Cannot add time slots after course is confirmed.");

        if (TimeSlots.Any(existing => existing == slot))
        {
            throw new InvalidOperationException("This time slot already exists.");
        }

        TimeSlots.Add(slot);
    }

    public void RemoveTimeSlot(TimeSlot slot)
    {
        if (IsConfirmed) throw new InvalidOperationException("Cannot remove time slots after course is confirmed.");

        TimeSlots.RemoveAll(t => t == slot);
    }

    public void Confirm()
    {
        if (IsConfirmed)
            throw new InvalidOperationException("Course is already confirmed.");

        if (TimeSlots.Count == 0)
            throw new InvalidOperationException("Course must have at least one time slot before confirmation.");

        if (StartDate > EndDate)
            throw new InvalidOperationException("Start date must be before end date.");

        if (string.IsNullOrWhiteSpace(Name))
            throw new InvalidOperationException("Course must have a name.");

        IsConfirmed = true;
    }

    public void AssignCoach(Coach coach)
    {
        if (!IsConfirmed)
            throw new InvalidOperationException("Cannot assign a coach before the course is confirmed.");

        if (AssignedCoach != null)
            throw new InvalidOperationException("A coach has already been assigned to this course.");

        if (!coach.IsSuitableFor(this))
            throw new InvalidOperationException("Coach does not have all the required skills for this course.");

        if (!coach.IsAvailableFor(this))
            throw new InvalidOperationException("Coach is not available during the scheduled course time slots.");

        AssignedCoach = coach;
        coach.AssignCourse(this);
    }

    public void UnassignCoach()
    {
        if (AssignedCoach == null) throw new InvalidOperationException("No coach has been assigned to this course.");

        AssignedCoach.UnassignCourse(this);
        AssignedCoach = null;
    }
}

