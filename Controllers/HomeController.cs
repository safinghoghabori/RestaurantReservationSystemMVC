using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using RestaurantReservationSystemMVC.Models;

namespace RestaurantReservationSystemMVC.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;
    private readonly IHttpContextAccessor _httpContextAccessor;

    public HomeController(ILogger<HomeController> logger, IHttpContextAccessor httpContextAccessor)
    {
        _logger = logger;
        _httpContextAccessor = httpContextAccessor;
    }

    public IActionResult Index()
    {
        return View();
    }

    public IActionResult Home()
    {
        var token = _httpContextAccessor.HttpContext.Session.GetString("JwtToken");
        var loggedInUserRole = _httpContextAccessor.HttpContext.Session.GetString("LoggedInUserRole");
        if (string.IsNullOrEmpty(token))
        {
            return RedirectToAction("Login", "Account");
        }
        ViewBag.LoggedInUserRole = loggedInUserRole;

        return View();
    }

    public IActionResult Privacy()
    {
        return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
