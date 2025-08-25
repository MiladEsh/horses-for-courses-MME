using HorsesForCourses.Api.Courses.UpdateTimeSlots;
using HorsesForCourses.Core.Domain;
using HorsesForCourses.Core.Domain.Courses;

namespace HorsesForCourses.Tests.Documentation.Courses;

public class CourseSpy : Course
{
    public CourseSpy() : base(TheCannonical.CourseName, TheCannonical.CourseStart, TheCannonical.CourseEnd) { }
    public bool SkillsCalled;
    public IEnumerable<string>? SkillsSeen;
    public override void UpdateRequiredSkills(IEnumerable<string> skills)
    {
        SkillsCalled = true; SkillsSeen = skills;
        base.UpdateRequiredSkills(skills);
    }

    public bool TimeSlotsCalled;
    public IEnumerable<TimeSlot>? TimeSlotsSeen;

    public override Course UpdateTimeSlots(IEnumerable<TimeSlot> timeSlots)
    {
        TimeSlotsCalled = true; TimeSlotsSeen = timeSlots;
        return base.UpdateTimeSlots(timeSlots);
    }
}