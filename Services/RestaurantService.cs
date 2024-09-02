using RestaurantReservationSystem.Mvc.Models;
using System.Net.Http.Headers;

public class RestaurantService : IRestaurantService
{
    private readonly HttpClient _httpClient;

    public RestaurantService(IHttpClientFactory httpClientFactory)
    {
        _httpClient = httpClientFactory.CreateClient("RestaurantApi");
    }

    private void AddAuthorizationHeader(string token)
    {
        if (!string.IsNullOrEmpty(token))
        {
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
        }
    }

    public async Task<List<Customer>> GetCustomersAsync(string token)
    {
        AddAuthorizationHeader(token);
        var response = await _httpClient.GetAsync("restaurant/customers");
        response.EnsureSuccessStatusCode();
        return await response.Content.ReadFromJsonAsync<List<Customer>>();
    }

    public async Task<List<Table>> GetTablesAsync(string token)
    {
        AddAuthorizationHeader(token);
        var response = await _httpClient.GetAsync("restaurant/tables");
        response.EnsureSuccessStatusCode();
        return await response.Content.ReadFromJsonAsync<List<Table>>();
    }

    public async Task<List<Reservation>> GetReservationsAsync()
    {
        var response = await _httpClient.GetAsync("restaurant/reservations");
        response.EnsureSuccessStatusCode();
        return await response.Content.ReadFromJsonAsync<List<Reservation>>();
    }

    public async Task AddCustomerAsync(Customer customer)
    {
        var response = await _httpClient.PostAsJsonAsync("restaurant/customers", customer);
        response.EnsureSuccessStatusCode();
    }

    public async Task AddTableAsync(Table table)
    {
        var response = await _httpClient.PostAsJsonAsync("restaurant/tables", table);
        response.EnsureSuccessStatusCode();
    }

    public async Task MakeReservationAsync(Reservation reservation)
    {
        var response = await _httpClient.PostAsJsonAsync("restaurant/reservations", reservation);
        response.EnsureSuccessStatusCode();
    }
}