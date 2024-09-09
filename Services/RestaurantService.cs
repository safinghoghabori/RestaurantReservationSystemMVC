using RestaurantReservationSystem.Mvc.Models;
using RestaurantReservationSystemMVC.Models;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Text.Json.Serialization;
using System.Text.Json;
using RestaurantReservationSystemMVC.Exceptions;

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

    // Customer-related methods
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
        return customers.Where(customers => customers.CustomerId == id).FirstOrDefault();
    }

    public async Task<List<Table>> GetTablesAsync(string token)
    {
        AddAuthorizationHeader(token);
        var response = await _httpClient.GetAsync("restaurant/tables");
        response.EnsureSuccessStatusCode();
        return await response.Content.ReadFromJsonAsync<List<Table>>();
    }
    public async Task AddCustomerAsync(Customer customer, string token)
    {
        AddAuthorizationHeader(token);
        var response = await _httpClient.PostAsJsonAsync("restaurant/customers", customer);
        response.EnsureSuccessStatusCode();
    }

    public async Task UpdateCustomerAsync(Customer customer, string token)
    {
        AddAuthorizationHeader(token);
        var response = await _httpClient.PutAsJsonAsync($"restaurant/customers/{customer.CustomerId}", customer);
        response.EnsureSuccessStatusCode();
    }

    public async Task DeleteCustomerAsync(int id, string token)
    {
        AddAuthorizationHeader(token);
        var response = await _httpClient.DeleteAsync($"restaurant/customers/{id}");
        response.EnsureSuccessStatusCode();
    }

    // Table-related methods
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

    // Reservation-related methods
    public async Task<List<Reservation>> GetReservationsAsync(string token)
    {
        AddAuthorizationHeader(token);
        var response = await _httpClient.GetAsync("restaurant/reservations");
        response.EnsureSuccessStatusCode();
        return await response.Content.ReadFromJsonAsync<List<Reservation>>();
    }

    public async Task<Reservation> GetReservationById(int id, string token)
    {
        var reservations = await GetReservationsAsync(token);
        var reservation = reservations.FirstOrDefault(res => res.ReservationId == id);
        return reservation;
    }

    public async Task AddReservationAsync(Reservation reservation, string token)
    {
        AddAuthorizationHeader(token);
        var response = await _httpClient.PostAsJsonAsync("restaurant/reservations", reservation);
        
        if(!response.IsSuccessStatusCode)
        {
            var errorResponse = await response.Content.ReadFromJsonAsync<ErrorResponse>();
            throw new ReservationException(errorResponse.Error);
        }
    }

    public async Task UpdateReservationAsync(Reservation reservation, string token)
    {
        AddAuthorizationHeader(token);
        var response = await _httpClient.PutAsJsonAsync($"restaurant/reservations/{reservation.ReservationId}", reservation);
        response.EnsureSuccessStatusCode();
    }

    public async Task DeleteReservationAsync(int id, string token)
    {
        AddAuthorizationHeader(token);
        var response = await _httpClient.DeleteAsync($"restaurant/reservations/{id}");
        response.EnsureSuccessStatusCode();
    }

    // Booking-related methods
    public async Task<List<Booking>> GetBookingsAsync(string token)
    {
        AddAuthorizationHeader(token);
        var response = await _httpClient.GetAsync("booking");
        response.EnsureSuccessStatusCode();
        return await response.Content.ReadFromJsonAsync<List<Booking>>();
    }

    public async Task<Booking> GetBookingById(int id, string token)
    {
        var bookings = await GetBookingsAsync(token);
        return bookings.FirstOrDefault(b => b.Bid == id);
    }

    public async Task AddBookingAsync(Booking booking, string token)
    {
        AddAuthorizationHeader(token);
        var response = await _httpClient.PostAsJsonAsync("booking", booking);
        response.EnsureSuccessStatusCode();
    }

    public async Task UpdateBookingAsync(Booking booking, string token)
    {
        AddAuthorizationHeader(token);
        var response = await _httpClient.PutAsJsonAsync($"booking/{booking.Bid}", booking);
        response.EnsureSuccessStatusCode();
    }

    public async Task DeleteBookingAsync(int id, string token)
    {
        AddAuthorizationHeader(token);
        var response = await _httpClient.DeleteAsync($"booking/{id}");
        response.EnsureSuccessStatusCode();
    }
}
