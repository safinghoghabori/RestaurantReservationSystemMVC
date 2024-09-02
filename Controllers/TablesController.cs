using Microsoft.AspNetCore.Mvc;
using RestaurantReservationSystem.Mvc.Models;

public class TablesController : Controller
{
    private readonly IRestaurantService _restaurantService;
    private readonly IHttpContextAccessor _httpContextAccessor;

    public TablesController(IRestaurantService restaurantService, IHttpContextAccessor httpContextAccessor)
    {
        _restaurantService = restaurantService;
        _httpContextAccessor = httpContextAccessor;
    }

    public async Task<IActionResult> Index()
    {
        var token = _httpContextAccessor.HttpContext.Session.GetString("JwtToken");
        if (string.IsNullOrEmpty(token))
        {
            return RedirectToAction(nameof(Login), "Account");
        }

        var tables = await _restaurantService.GetTablesAsync(token);
        return View(tables);
    }

    public IActionResult Create()
    {
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> Create(Table table)
    {
        if (ModelState.IsValid)
        {
            var token = _httpContextAccessor.HttpContext.Session.GetString("JwtToken");
            await _restaurantService.AddTableAsync(token, table);
            return RedirectToAction(nameof(Index));
        }

        return View();
    }
}