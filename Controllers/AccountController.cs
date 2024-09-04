using Microsoft.AspNetCore.Mvc;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;

/// <summary>
    /// The AccountController class handles user authentication, including login and logout operations. 
    /// It uses IHttpClientFactory to make HTTP requests and IHttpContextAccessor to manage session data. 
    /// Login (GET): Displays the login view.
    /// Login (POST): Authenticates the user by sending a login request to the API. If successful, stores the JWT in the session and redirects to the home page. Otherwise, displays an error message.
    /// Logout: Clears the session and redirects to the login view.
/// </summary>

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
    public async Task<IActionResult> Login(Login model)
    {
        if (!ModelState.IsValid)
        {
            return View(model);
        }

        var client = _httpClientFactory.CreateClient("RestaurantApi");

        var loginRequest = new
        {
            Username = model.Username,
            Password = model.Password
        };

        var content = new StringContent(JsonSerializer.Serialize(loginRequest), Encoding.UTF8, "application/json");

        var response = await client.PostAsync("auth/login", content);

        if (response.IsSuccessStatusCode)
        {
            var responseContent = await response.Content.ReadAsStringAsync();
            var jwt = JsonSerializer.Deserialize<JsonElement>(responseContent)
                        .GetProperty("token").GetString();

            // Save the JWT in the session
            _httpContextAccessor.HttpContext.Session.SetString("JwtToken", jwt);

            return RedirectToAction("Home", "Home");
        }

        ModelState.AddModelError(string.Empty, "Invalid login attempt.");
        return View(model);
    }

    [HttpPost]
    public IActionResult Logout()
    {
        // Clear the session
        _httpContextAccessor.HttpContext.Session.Clear();
        return RedirectToAction("Login");
    }
}
