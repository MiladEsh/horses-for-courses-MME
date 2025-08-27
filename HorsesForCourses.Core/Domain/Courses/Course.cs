using HorsesForCourses.Core.Abstractions;
using HorsesForCourses.Core.Domain.Coaches;
using HorsesForCourses.Core.Domain.Courses.InvalidationReasons;
using HorsesForCourses.Core.Domain.Courses.TimeSlots;
using HorsesForCourses.Core.Domain.Skills;

namespace HorsesForCourses.Core.Domain.Courses;

public class Course : DomainEntity<Course>
{
    public string Name { get; private set; }
    public DateOnly StartDate { get; private set; }
    public DateOnly EndDate { get; private set; }
    public List<TimeSlot> TimeSlots { get; private set; } = new();
    public List<Skill> RequiredSkills { get; private set; } = new();
    public bool IsConfirmed { get; private set; }

    public Coach? AssignedCoach { get; private set; }

    public Course(string name, DateOnly startDate, DateOnly endDate)
    {
        if (string.IsNullOrWhiteSpace(name))
            throw new CourseNameCanNotBeEmpty();

        if (startDate > endDate)
            throw new CourseEndDateCanNotBeBeforeStartDate();

        Name = name;
        StartDate = startDate;
        EndDate = endDate;
    }

    public virtual Course UpdateRequiredSkills(IEnumerable<string> skills)
    {
        if (IsConfirmed)
            throw new CourseAlreadyConfirmed();

        var duplicates = skills
            .GroupBy(x => x)
            .Where(g => g.Count() > 1)
            .SelectMany(g => g)
            .Distinct()
            .ToList();

        if (duplicates.Any())
            throw new CourseAlreadyHasSkill(string.Join(",", skills));

        var newSkills = skills.Select(Skill.From).ToList();

        RequiredSkills.Clear();
        RequiredSkills.AddRange(newSkills);

        return this;
    }

    public virtual Course UpdateTimeSlots<T>(IEnumerable<T> timeSlotInfo, Func<T, (CourseDay Day, int Start, int End)> getTimeSlot)
    {
        var timeSlots = TimeSlot.EnumerableFrom(timeSlotInfo, getTimeSlot);
        if (IsConfirmed)
            throw new CourseAlreadyConfirmed();
        if (TimeSlotsOverlap(timeSlots))
            throw new OverlappingTimeSlots();
        TimeSlots = [.. timeSlots];
        return this;
    }

    private bool TimeSlotsOverlap(IEnumerable<TimeSlot> timeSlots)
    {
        var index = 1;
        foreach (var timeSlot in timeSlots)
        {
            foreach (var otherTimeSlot in timeSlots.Skip(index))
            {
                if (timeSlot.OverlapsWith(otherTimeSlot))
                    return true;
            }
            index++;
        }
        return false;
    }

    public Course Confirm()
    {
        if (IsConfirmed)
            throw new CourseAlreadyConfirmed();

        if (TimeSlots.Count == 0)
            throw new AtLeastOneTimeSlotRequired();

        IsConfirmed = true;

        return this;
    }

    public virtual Course AssignCoach(Coach coach)
    {
        if (!IsConfirmed)
            throw new CourseNotYetConfirmed();

        if (AssignedCoach != null)
            throw new CourseAlreadyHasCoach();

        if (!coach.IsSuitableFor(this))
            throw new CoachNotSuitableForCourse();

        if (!coach.IsAvailableFor(this))
            throw new CoachNotAvailableForCourse();

        AssignedCoach = coach;
        coach.AssignCourse(this);
        return this;
    }
}
