using HorsesForCourses.Service;
using HorsesForCourses.Service.Coaches.GetCoachDetail;
using HorsesForCourses.Service.Coaches.GetCoaches;
using HorsesForCourses.MVC.ViewModels;
using Microsoft.AspNetCore.Mvc;
using HorsesForCourses.Service.Coaches;

namespace HorsesForCourses.MVC.Controllers;

public class CoachesController : Controller
{
    private readonly CoachesService coachesService;

    public CoachesController(
        CoachesService coachesService)
    {
        this.coachesService = coachesService;
    }

    [HttpPost("RegisterCoach")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> RegisterCoach(RegisterCoachViewModel model)
    {
        if (!ModelState.IsValid)
        {
            return View(model);
        }
        try
        {
            await coachesService.RegisterCoach(model.Name, model.Email);
            return RedirectToAction(nameof(Index));
        }
        catch (Exception)
        {
            ModelState.AddModelError("", "Er is iets fout gegaan bij het registreren van de coach.");
            return View(model);
        }
    }

    [HttpGet]
    public async Task<IActionResult> Index()
    {
        if (coachesService.GetCoaches == null)
        {
            // Handle the null case, e.g., show an error view or return an empty list
            ModelState.AddModelError("", "Coach service is not available.");
            return View(new List<CoachViewModel>());
        }

        var coaches = await coachesService.GetCoaches.All(new(1, 25));
        return View(coaches);
    }

    [HttpGet]
    public async Task<IActionResult> Details(int id)
    {
        if (coachesService.GetCoachDetail == null)
        {
            ModelState.AddModelError("", "Coach detail service is not available.");
            return View("Error");
        }

        var coach = await coachesService.GetCoachDetail.One(id);
        if (coach == null)
        {
            return NotFound();
        }
        return View(coach);
    }
}