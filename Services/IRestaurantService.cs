using RestaurantReservationSystem.Mvc.Models;

public interface IRestaurantService
{
    Task<List<Customer>> GetCustomersAsync(string token);
    Task<List<Table>> GetTablesAsync(string token);
    Task<List<Reservation>> GetReservationsAsync();
    Task AddCustomerAsync(Customer customer);
    Task AddTableAsync(string token, Table table);
    Task MakeReservationAsync(Reservation reservation);
}
