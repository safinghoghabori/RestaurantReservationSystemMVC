using Microsoft.AspNetCore.Mvc;
using RestaurantReservationSystem.Mvc.Models;
using System.Threading.Tasks;

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
