using Microsoft.AspNetCore.Mvc;
using RestaurantReservationSystem.Mvc.Models;


/// <summary>
    /// The TablesController class manages table-related operations such as listing, creating, updating, and deleting tables.
    /// It uses IRestaurantService to interact with the backend and IHttpContextAccessor to manage session data. 
    /// Index: Retrieves and displays a list of tables. Redirects to login if the JWT token is missing.
    /// Create (GET): Displays the form to create a new table.
    /// Create (POST): Adds a new table if the model state is valid. Redirects to login if the JWT token is missing.
    /// Update (GET): Retrieves and displays the table details for updating. Redirects to login if the JWT token is missing.
    /// Update (POST): Updates the table details if the model state is valid. Redirects to login if the JWT token is missing.
    /// Delete: Deletes a table by its ID. Redirects to login if the JWT token is missing.
/// </summary>

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

    public async Task<IActionResult> Update(int id)
    {
        var token = _httpContextAccessor.HttpContext.Session.GetString("JwtToken");
        if (string.IsNullOrEmpty(token))
        {
            return RedirectToAction(nameof(Login), "Account");
        }

        var table = await _restaurantService.GetTableById(id, token);
        return View(table);
    }

    [HttpPost]
    public async Task<IActionResult> Update(Table table)
    {
        var token = _httpContextAccessor.HttpContext.Session.GetString("JwtToken");
        if (string.IsNullOrEmpty(token))
        {
            return RedirectToAction(nameof(Login), "Account");
        }

        if (ModelState.IsValid)
        {
            await _restaurantService.UpdateTable(table, token);
            return RedirectToAction(nameof(Index));
        }

        return View(table);
    }

    public async Task<IActionResult> Delete(int id)
    {
        var token = _httpContextAccessor.HttpContext.Session.GetString("JwtToken");
        await _restaurantService.DeleteTable(id, token);
        return RedirectToAction(nameof(Index));
    }

}