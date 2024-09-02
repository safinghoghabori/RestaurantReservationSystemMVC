using Microsoft.AspNetCore.Mvc;
using RestaurantReservationSystem.Mvc.Models;

public class CustomersController : Controller
{
    private readonly IRestaurantService _restaurantService;
    private readonly IHttpContextAccessor _httpContextAccessor;

    public CustomersController(IRestaurantService restaurantService, IHttpContextAccessor httpContextAccessor)
    {
        _restaurantService = restaurantService;
        _httpContextAccessor = httpContextAccessor;
    }

    public async Task<IActionResult> Index()
    {
        var token = _httpContextAccessor.HttpContext.Session.GetString("JwtToken");
        if (string.IsNullOrEmpty(token))
        {
            return RedirectToAction("Login", "Account");
        }

        var customers = await _restaurantService.GetCustomersAsync(token);
        return View(customers);
    }

    public IActionResult Create()
    {
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> Create(Customer customer)
    {
        if (ModelState.IsValid)
        {
            await _restaurantService.AddCustomerAsync(customer);
            return RedirectToAction(nameof(Index));
        }
        return View(customer);
    }
}
