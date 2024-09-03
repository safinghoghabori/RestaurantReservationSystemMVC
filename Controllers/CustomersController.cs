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
        var token = _httpContextAccessor.HttpContext.Session.GetString("JwtToken");
        if (string.IsNullOrEmpty(token))
        {
            return RedirectToAction("Login", "Account");
        }

        if (ModelState.IsValid)
        {
            await _restaurantService.AddCustomerAsync(customer, token);
            return RedirectToAction(nameof(Index));
        }
        return View(customer);
    }
    [HttpGet]
    public async Task<IActionResult> Edit(int id)
    {
        var token = _httpContextAccessor.HttpContext.Session.GetString("JwtToken");
        if (string.IsNullOrEmpty(token))
        {
            return RedirectToAction("Login", "Account");
        }

        var customer = await _restaurantService.GetCustomerById(id, token);
        if (customer == null)
        {
            return NotFound();
        }

        return View(customer);
    }

    [HttpPost]
    public async Task<IActionResult> Edit(Customer customer)
    {
        var token = _httpContextAccessor.HttpContext.Session.GetString("JwtToken");
        if (string.IsNullOrEmpty(token))
        {
            return RedirectToAction("Login", "Account");
        }

        if (ModelState.IsValid)
        {
            var existingCustomer = await _restaurantService.GetCustomerById(customer.CustomerId, token);
            if (existingCustomer == null)
            {
                return NotFound();
            }

            existingCustomer.Name = customer.Name;
            existingCustomer.PhoneNumber = customer.PhoneNumber;
            existingCustomer.Age = customer.Age;
            existingCustomer.Gender = customer.Gender;

            await _restaurantService.UpdateCustomerAsync(existingCustomer, token);
            return RedirectToAction(nameof(Index));
        }

        return View(customer);
    }
    // POST: Delete Customer
    public async Task<IActionResult> Delete(int id)
    {
        var token = _httpContextAccessor.HttpContext.Session.GetString("JwtToken");
        await _restaurantService.DeleteCustomerAsync(id, token);
        return RedirectToAction(nameof(Index));
    }
}



