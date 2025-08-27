using HorsesForCourses.Core.Abstractions;
using HorsesForCourses.Core.Domain.Coaches;
using HorsesForCourses.Core.Domain.Courses.InvalidationReasons;
using HorsesForCourses.Core.Domain.Courses.TimeSlots;
using HorsesForCourses.Core.Domain.Skills;
using HorsesForCourses.Core.Extensions;

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

    private bool NotAllowedIfAlreadyConfirmed()
        => IsConfirmed ? throw new CourseAlreadyConfirmed() : true;

    private static bool NotAllowedWhenThereAreDuplicateSkills(IEnumerable<string> skills)
        => skills.NoDuplicatesAllowed(a => new CourseAlreadyHasSkill(string.Join(",", a)));

    public virtual Course UpdateRequiredSkills(IEnumerable<string> skills)
    {
        NotAllowedIfAlreadyConfirmed();
        NotAllowedWhenThereAreDuplicateSkills(skills);
        var newSkills = skills.Select(Skill.From).ToList();
        RequiredSkills.Clear();
        RequiredSkills.AddRange(newSkills);
        return this;
    }

    private bool NotAllowedWhenTimeSlotsOverlap(IEnumerable<TimeSlot> timeSlots)
        => TimeSlot.HasOverlap(timeSlots) ? throw new OverlappingTimeSlots() : true;

    public virtual Course UpdateTimeSlots<T>(IEnumerable<T> timeSlotInfo, Func<T, (CourseDay Day, int Start, int End)> getTimeSlot)
    {
        var timeSlots = TimeSlot.EnumerableFrom(timeSlotInfo, getTimeSlot);
        NotAllowedIfAlreadyConfirmed();
        NotAllowedWhenTimeSlotsOverlap(timeSlots);
        TimeSlots = [.. timeSlots];
        return this;
    }

    private bool NotAllowedWhenThereAreNoTimeSlots()
        => TimeSlots.Count == 0 ? throw new AtLeastOneTimeSlotRequired() : true;

    public Course Confirm()
    {
        NotAllowedIfAlreadyConfirmed();
        NotAllowedWhenThereAreNoTimeSlots();
        IsConfirmed = true;
        return this;
    }

    private bool NotAllowedIfNotYetConfirmed()
        => !IsConfirmed ? throw new CourseNotYetConfirmed() : true;

    private bool NotAllowedIfCourseAlreadyHasCoach()
        => AssignedCoach != null ? throw new CourseAlreadyHasCoach() : true;

    public virtual Course AssignCoach(Coach coach)
    {
        NotAllowedIfNotYetConfirmed();
        NotAllowedIfCourseAlreadyHasCoach();
        if (!coach.IsSuitableFor(this))
            throw new CoachNotSuitableForCourse();
        if (!Is.TheCoach(coach).AvailableFor(this))
            throw new CoachNotAvailableForCourse();
        AssignedCoach = coach;
        coach.AssignCourse(this);
        return this;
    }
}
