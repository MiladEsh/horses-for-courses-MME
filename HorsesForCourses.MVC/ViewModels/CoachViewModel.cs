using HorsesForCourses.Core.Abstractions;
using HorsesForCourses.Core.Domain.Coaches;

namespace HorsesForCourses.MVC.ViewModels;

public record CoachViewModel(Id<Coach> Id, string Name, string Email);