using Microsoft.AspNetCore.Mvc;
using RestaurantReservationSystem.Mvc.Models;


/// <summary>
    /// The CustomersController class manages customer-related operations such as listing, creating, editing, and deleting customers.
    /// It uses IRestaurantService to interact with the backend and IHttpContextAccessor to manage session data.
    /// Index: Retrieves and displays a list of customers. Redirects to login if the JWT token is missing.
    /// Create (GET): Displays the form to create a new customer.
    /// Create (POST): Adds a new customer if the model state is valid. Redirects to login if the JWT token is missing.
    /// Edit (GET): Retrieves and displays the customer details for editing. Redirects to login if the JWT token is missing.
    /// Edit (POST): Updates the customer details if the model state is valid. Redirects to login if the JWT token is missing.
    /// Delete: Deletes a customer by their ID. Redirects to login if the JWT token is missing.
/// </summary>

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



