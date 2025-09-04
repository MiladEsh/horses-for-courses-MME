using HorsesForCourses.Core.Abstractions;
using HorsesForCourses.Core.Domain.Coaches;
using HorsesForCourses.Service.Coaches.GetCoaches;
using Microsoft.AspNetCore.Mvc;

namespace HorsesForCourses.MVC.Controllers;

public class CoachesController : Controller
{
    private readonly IGetCoachSummaries _getCoaches;

    public CoachesController(IGetCoachSummaries getCoaches)
    {
        _getCoaches = getCoaches;
    }

    public async Task<IActionResult> Index()
    {
        var coaches = await _getCoaches.All(new(1, 25));
        return View(coaches);
    }

    public IActionResult Details(Id<Coach> id)
    {
        return Content($"Details voor coach met ID {id}");
    }
}