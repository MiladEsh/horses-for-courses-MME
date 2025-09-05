
using HorsesForCourses.Service.Coaches.GetCoachDetail;
using HorsesForCourses.Service.Coaches.GetCoaches;
using Microsoft.AspNetCore.Mvc;

namespace HorsesForCourses.MVC.Controllers;

public class CoachesController : Controller
{
    private readonly IGetCoachSummaries _getCoaches;
    private readonly GetCoachDetail _getCoachDetail;

    public CoachesController(
        IGetCoachSummaries getCoaches,
        GetCoachDetail getCoachDetail)
    {
        _getCoaches = getCoaches;
        _getCoachDetail = getCoachDetail;
    }

    [HttpGet]
    public async Task<IActionResult> CreateCoach()
    {
        return View();
    }

    [HttpGet]
    public async Task<IActionResult> Index()
    {
        var coaches = await _getCoaches.All(new(1, 25));
        return View(coaches);
    }

    [HttpGet]
    public async Task<IActionResult> Details(int id)
    {
        var coach = await _getCoachDetail.One(id);
        return View(coach);
    }
}