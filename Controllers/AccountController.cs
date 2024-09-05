using Microsoft.AspNetCore.Mvc;
using System.Net;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;

public class AccountController : Controller
{
    private readonly IHttpClientFactory _httpClientFactory;
    private readonly IHttpContextAccessor _httpContextAccessor;

    public AccountController(IHttpClientFactory httpClientFactory, IHttpContextAccessor httpContextAccessor)
    {
        _httpClientFactory = httpClientFactory;
        _httpContextAccessor = httpContextAccessor;
    }

    [HttpGet]
    public IActionResult Login()
    {
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> Login(Login user)
    {
        if (!ModelState.IsValid)
        {
            return View(user);
        }

        var client = _httpClientFactory.CreateClient("RestaurantApi");

        var response = await client.PostAsJsonAsync("auth/login", user);

        if (response.IsSuccessStatusCode)
        {
            var responseContent = await response.Content.ReadAsStringAsync();
            var jwt = JsonSerializer.Deserialize<JsonElement>(responseContent)
                        .GetProperty("token").GetString();

            // Save the JWT in the session
            _httpContextAccessor.HttpContext.Session.SetString("JwtToken", jwt);

            return RedirectToAction("Home", "Home");
        }

        if (response.StatusCode == HttpStatusCode.Unauthorized)
        {
            ModelState.AddModelError("ApiError", "Invalid credentials. Please try again.");
        }
        else
        {
            ModelState.AddModelError("ApiError", "An error occurred during login. Please try again.");
        }

        return View(user);
    }

    [HttpPost]
    public IActionResult Logout()
    {
        // Clear the session
        _httpContextAccessor.HttpContext.Session.Clear();
        return RedirectToAction("Login");
    }
}
