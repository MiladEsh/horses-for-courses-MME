using HorsesForCourses.Core.Domain.Coaches;
using Moq;

namespace HorsesForCourses.Tests;

public static class TheCannonical
{
    public const string CoachName = "a";
    public const string CoachEmail = "a@a.a";
    public static Coach Coach()
        => new(CoachName, CoachEmail);
    public static Coach CheckCoachNameAndEmail
        = It.Is<Coach>(a => a.Name == CoachName && a.Email == CoachEmail);
}