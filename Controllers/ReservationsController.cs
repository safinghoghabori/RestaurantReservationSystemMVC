using Microsoft.AspNetCore.Mvc;
using RestaurantReservationSystem.Mvc.Models;
using System.Threading.Tasks;

/// <summary>
    /// The ReservationsController class manages reservation-related operations such as listing, creating, editing, and deleting reservations.
    /// It uses IRestaurantService to interact with the backend and IHttpContextAccessor to manage session data. 
    /// Index: Retrieves and displays a list of reservations. Redirects to login if the JWT token is missing.
    /// Create (GET): Displays the form to create a new reservation. Redirects to login if the JWT token is missing.
    /// Create (POST): Adds a new reservation if the model state is valid. Redirects to login if the JWT token is missing.
    /// Edit (GET): Retrieves and displays the reservation details for editing. Redirects to login if the JWT token is missing.
    /// Edit (POST): Updates the reservation details if the model state is valid. Redirects to login if the JWT token is missing.
    /// Delete: Deletes a reservation by its ID. Redirects to login if the JWT token is missing.
/// </summary>

namespace RestaurantReservationSystem.Mvc.Controllers
{
    public class ReservationsController : Controller
    {
        private readonly IRestaurantService _restaurantService;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public ReservationsController(IRestaurantService restaurantService, IHttpContextAccessor httpContextAccessor)
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

            var reservations = await _restaurantService.GetReservationsAsync(token);
            return View(reservations);
        }

        public async Task<IActionResult> Create()
        {
            var token = _httpContextAccessor.HttpContext.Session.GetString("JwtToken");
            if (string.IsNullOrEmpty(token))
            {
                return RedirectToAction("Login", "Account");
            }

            ViewBag.Customers = await _restaurantService.GetCustomersAsync(token);
            ViewBag.Tables = await _restaurantService.GetTablesAsync(token);
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(Reservation reservation)
        {
            var token = _httpContextAccessor.HttpContext.Session.GetString("JwtToken");
            if (string.IsNullOrEmpty(token))
            {
                return RedirectToAction("Login", "Account");
            }

            if (ModelState.IsValid)
            {
                await _restaurantService.AddReservationAsync(reservation, token);
                return RedirectToAction(nameof(Index));
            }

            ViewBag.Customers = await _restaurantService.GetCustomersAsync(token);
            ViewBag.Tables = await _restaurantService.GetTablesAsync(token);
            return View(reservation);
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var token = _httpContextAccessor.HttpContext.Session.GetString("JwtToken");
            if (string.IsNullOrEmpty(token))
            {
                return RedirectToAction("Login", "Account");
            }

            var reservation = await _restaurantService.GetReservationById(id, token);
            if (reservation == null)
            {
                return NotFound();
            }

            ViewBag.Customers = await _restaurantService.GetCustomersAsync(token);
            ViewBag.Tables = await _restaurantService.GetTablesAsync(token);
            return View(reservation);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(Reservation reservation)
        {
            var token = _httpContextAccessor.HttpContext.Session.GetString("JwtToken");
            if (string.IsNullOrEmpty(token))
            {
                return RedirectToAction("Login", "Account");
            }

            if (ModelState.IsValid)
            {
                await _restaurantService.UpdateReservationAsync(reservation, token);
                return RedirectToAction(nameof(Index));
            }

            ViewBag.Customers = await _restaurantService.GetCustomersAsync(token);
            ViewBag.Tables = await _restaurantService.GetTablesAsync(token);
            return View(reservation);
        }


        public async Task<IActionResult> Delete(int id)
        {
            var token = _httpContextAccessor.HttpContext.Session.GetString("JwtToken");
            await _restaurantService.DeleteReservationAsync(id, token);
            return RedirectToAction(nameof(Index));
        }
    }
}
