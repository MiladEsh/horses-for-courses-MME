using HorsesForCourses.Core.Domain;
using HorsesForCourses.Core.Domain.Coaches;
using HorsesForCourses.Core.Domain.Courses;

namespace HorsesForCourses.Tests.Tools.Courses;

public class CourseSpy : Course
{
    public CourseSpy() : base(TheCanonical.CourseName, TheCanonical.CourseStart, TheCanonical.CourseEnd) { }
    public bool SkillsCalled;
    public IEnumerable<string>? SkillsSeen;
    public override Course UpdateRequiredSkills(IEnumerable<string> skills)
    {
        SkillsCalled = true; SkillsSeen = skills;
        base.UpdateRequiredSkills(skills);
        return this;
    }

    public bool TimeSlotsCalled;
    public IEnumerable<TimeSlot>? TimeSlotsSeen;

    public override Course UpdateTimeSlots(IEnumerable<TimeSlot> timeSlots)
    {
        TimeSlotsCalled = true; TimeSlotsSeen = timeSlots;
        return base.UpdateTimeSlots(timeSlots);
    }

    public bool AssignCoachCalled;
    public Coach? AssignCoachSeen;
    public override Course AssignCoach(Coach coach)
    {
        AssignCoachCalled = true; AssignCoachSeen = coach;
        base.AssignCoach(coach);
        return this;
    }
}