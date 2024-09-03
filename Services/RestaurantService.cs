using RestaurantReservationSystem.Mvc.Models;
using System.Net.Http.Headers;
using System.Net.Http.Json;

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

    public async Task<Customer> GetCustomerById(int id, string token)
    {
        var customers = await GetCustomersAsync(token);
        return customers.Where(customer => customer.CustomerId == id).FirstOrDefault();
    }

    public async Task UpdateCustomerAsync(Customer customer, string token)
    {
        AddAuthorizationHeader(token);
        var response = await _httpClient.PutAsJsonAsync($"restaurant/customers/{customer.CustomerId}", customer);
        response.EnsureSuccessStatusCode();
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

    public async Task AddCustomerAsync(Customer customer, string token)
    {
        AddAuthorizationHeader(token);
        var response = await _httpClient.PostAsJsonAsync("restaurant/customers", customer);
        response.EnsureSuccessStatusCode();
    }

    public async Task AddTableAsync(string token, Table table)
    {
        AddAuthorizationHeader(token);
        var response = await _httpClient.PostAsJsonAsync("restaurant/tables", table);
        response.EnsureSuccessStatusCode();
    }

    public async Task<Table> GetTableById(int id, string token)
    {
        var tables = await GetTablesAsync(token);
        return tables.FirstOrDefault(tab => tab.TableId == id);
    }

    public async Task UpdateTable(Table table, string token)
    {
        AddAuthorizationHeader(token);
        var response = await _httpClient.PutAsJsonAsync("restaurant/tables", table);
    }

    public async Task DeleteTable(int id, string token)
    {
        AddAuthorizationHeader(token);
        await _httpClient.DeleteAsync($"restaurant/tables/{id}");
    }

    public async Task MakeReservationAsync(Reservation reservation)
    {
        var response = await _httpClient.PostAsJsonAsync("restaurant/reservations", reservation);
        response.EnsureSuccessStatusCode();
    }
    public async Task DeleteCustomerAsync(int id, string token)
    {
        AddAuthorizationHeader(token);
        var response = await _httpClient.DeleteAsync($"restaurant/customers/{id}");
        response.EnsureSuccessStatusCode();
    }
}
