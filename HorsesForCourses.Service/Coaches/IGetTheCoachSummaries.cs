using HorsesForCourses.Service.Coaches.Models;

namespace HorsesForCourses.Service.Coaches;

public interface IGetTheCoachSummaries
{
    Task<IReadOnlyList<CoachSummaryDto>> Execute();
}