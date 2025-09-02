using HorsesForCourses.Core.Abstractions;
using HorsesForCourses.Core.Domain.Coaches;

namespace HorsesForCourses.Service.Coaches.Models;

public record CoachSummaryDto(Id<Coach> Id, string Name, string Email);