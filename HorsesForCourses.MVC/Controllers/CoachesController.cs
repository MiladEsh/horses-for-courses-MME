using HorsesForCourses.Core.Abstractions;
using HorsesForCourses.Core.Domain.Coaches;
using HorsesForCourses.Service.Coaches;
using Microsoft.AspNetCore.Mvc;

namespace HorsesForCourses.MVC.Controllers;

public class CoachesController : Controller
{
    private readonly IGetTheCoachSummaries _getCoaches;

    public CoachesController(IGetTheCoachSummaries getCoaches)
    {
        _getCoaches = getCoaches;
    }

    public async Task<IActionResult> Index()
    {
        var coaches = await _getCoaches.Execute();
        return View(coaches);
    }

    public IActionResult Details(Id<Coach> id)
    {
        return Content($"Details voor coach met ID {id}");
    }
}