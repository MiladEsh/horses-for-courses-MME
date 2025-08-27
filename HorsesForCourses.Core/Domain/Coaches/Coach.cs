using HorsesForCourses.Core.Abstractions;
using HorsesForCourses.Core.Domain.Coaches.InvalidationReasons;
using HorsesForCourses.Core.Domain.Courses;
using HorsesForCourses.Core.Domain.Courses.TimeSlots;
using HorsesForCourses.Core.Domain.Skills;

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
        if (string.IsNullOrWhiteSpace(name))
            throw new CoachNameCanNotBeEmpty();
        if (string.IsNullOrWhiteSpace(email))
            throw new CoachEmailCanNotBeEmpty();

        Name = name;
        Email = email;
    }

    public virtual void UpdateSkills(IEnumerable<string> skills)
    {
        var duplicates = skills
            .GroupBy(x => x)
            .Where(g => g.Count() > 1)
            .SelectMany(g => g)
            .Distinct()
            .ToList();

        if (duplicates.Any())
            throw new CoachAlreadyHasSkill(string.Join(",", skills));

        Skills.Clear();
        skills.Select(a => Skill.From(a))
            .ToList()
            .ForEach(a => Skills.Add(a));
    }

    public bool IsSuitableFor(Course course)
    {
        return course.RequiredSkills.All(Skills.Contains);
    }

    public void AssignCourse(Course course)
    {
        assignedCourses.Add(course);
    }

    public bool IsAvailableFor(Course course)
    {
        foreach (var assigned in assignedCourses)
        {
            if (CoursesOverlap(course, assigned))
            {
                return false;
            }
        }
        return true;
    }

    private static bool CoursesOverlap(Course courseOne, Course courseTwo)
    {
        var start = Max(courseOne.StartDate, courseTwo.StartDate);
        var end = Min(courseOne.EndDate, courseTwo.EndDate);
        if (end < start) return false;

        var courseOneByDay = GetTimeSlotsByDay(courseOne);
        var courseTwoByDay = GetTimeSlotsByDay(courseTwo);

        foreach (CourseDay day in Enum.GetValues(typeof(CourseDay)))
        {
            if (!courseOneByDay.TryGetValue(day, out var slotsOne))
                continue;

            if (!courseTwoByDay.TryGetValue(day, out var slotsTwo))
                continue;

            var firstOccurrence = NextOnOrAfter(start, ToDayOfWeek(day));
            if (firstOccurrence > end)
                continue;

            foreach (var slotOne in slotsOne)
                foreach (var slotTwo in slotsTwo)
                    if (slotOne.OverlapsWith(slotTwo))
                        return true;
        }

        return false;
    }

    private static Dictionary<CourseDay, List<TimeSlot>> GetTimeSlotsByDay(Course courseOne)
    {
        return courseOne.TimeSlots
            .GroupBy(t => t.Day)
            .ToDictionary(g => g.Key, g => g.ToList());
    }

    private static DayOfWeek ToDayOfWeek(CourseDay day)
    {
        return day switch
        {
            CourseDay.Monday => DayOfWeek.Monday,
            CourseDay.Tuesday => DayOfWeek.Tuesday,
            CourseDay.Wednesday => DayOfWeek.Wednesday,
            CourseDay.Thursday => DayOfWeek.Thursday,
            CourseDay.Friday => DayOfWeek.Friday,
            _ => throw new NotImplementedException()
        };
    }

    private static DateOnly NextOnOrAfter(DateOnly date, DayOfWeek target)
    {
        var diff = ((int)target - (int)date.DayOfWeek + 7) % 7;
        return date.AddDays(diff);
    }

    private static DateOnly Max(DateOnly x, DateOnly y) => x > y ? x : y;
    private static DateOnly Min(DateOnly x, DateOnly y) => x < y ? x : y;
}
