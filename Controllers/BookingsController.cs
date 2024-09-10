using Microsoft.AspNetCore.Mvc;
using RestaurantReservationSystem.Mvc.Models;
using System.Threading.Tasks;

public class BookingsController : Controller
{
    private readonly IRestaurantService _restaurantService;
    private readonly IHttpContextAccessor _httpContextAccessor;

    public BookingsController(IRestaurantService restaurantService, IHttpContextAccessor httpContextAccessor)
    {
        _restaurantService = restaurantService;
        _httpContextAccessor = httpContextAccessor;
    }

    public async Task<IActionResult> Index()
    {
        var token = _httpContextAccessor.HttpContext.Session.GetString("JwtToken");
        var loggedInUserRole = _httpContextAccessor.HttpContext.Session.GetString("LoggedInUserRole");
        if (string.IsNullOrEmpty(token))
        {
            return RedirectToAction("Login", "Account");
        }
        ViewBag.LoggedInUserRole = loggedInUserRole;

        var bookings = await _restaurantService.GetBookingsAsync(token);
        return View(bookings);
    }

    public IActionResult Create()
    {
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> Create(Booking booking)
    {
        var token = _httpContextAccessor.HttpContext.Session.GetString("JwtToken");
        if (string.IsNullOrEmpty(token))
        {
            return RedirectToAction("Login", "Account");
        }

        if (ModelState.IsValid)
        {
            await _restaurantService.AddBookingAsync(booking, token);
            return RedirectToAction(nameof(Index));
        }
        return View(booking);
    }

    [HttpGet]
    public async Task<IActionResult> Edit(int id)
    {
        var token = _httpContextAccessor.HttpContext.Session.GetString("JwtToken");
        if (string.IsNullOrEmpty(token))
        {
            return RedirectToAction("Login", "Account");
        }

        var booking = await _restaurantService.GetBookingById(id, token);
        if (booking == null)
        {
            return NotFound();
        }

        return View(booking);
    }

    [HttpPost]
    public async Task<IActionResult> Edit(Booking booking)
    {
        var token = _httpContextAccessor.HttpContext.Session.GetString("JwtToken");
        if (string.IsNullOrEmpty(token))
        {
            return RedirectToAction("Login", "Account");
        }

        if (ModelState.IsValid)
        {
            var existingBooking = await _restaurantService.GetBookingByEmail(booking.Email, token);
            if (existingBooking == null)
            {
                return NotFound();
            }

            existingBooking.Name = booking.Name;
            existingBooking.Age = booking.Age;
            existingBooking.Gender = booking.Gender;
            existingBooking.PhoneNumber = booking.PhoneNumber;
            existingBooking.DateTime = booking.DateTime;
            existingBooking.Capacity = booking.Capacity;

            await _restaurantService.UpdateBookingAsync(existingBooking, token);
            return RedirectToAction(nameof(Index));
        }

        return View(booking);
    }

    [HttpGet]
    public async Task<IActionResult> Delete(int id)
    {
        var token = _httpContextAccessor.HttpContext.Session.GetString("JwtToken");
        await _restaurantService.DeleteBookingAsync(id, token);
        return RedirectToAction(nameof(Index));
    }
}
