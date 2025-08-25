using HorsesForCourses.Core.Domain.Coaches;
using HorsesForCourses.Core.Domain.Courses;
using Moq;
using WibblyWobbly;

namespace HorsesForCourses.Tests;

public static class TheCannonical
{
    public const string CoachName = "a";
    public const string CoachEmail = "a@a.a";
    public static Coach Coach()
        => new(CoachName, CoachEmail);


    public const string CourseName = "A";
    public static readonly DateOnly CourseStart = DateOnly.FromDateTime(1.January(2025));
    public static readonly DateOnly CourseEnd = DateOnly.FromDateTime(31.January(2025));

    public static Course Course()
        => new(CourseName, CourseStart, CourseEnd);
}