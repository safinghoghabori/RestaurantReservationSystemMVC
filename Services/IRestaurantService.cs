using RestaurantReservationSystem.Mvc.Models;

public interface IRestaurantService
{
    Task<List<Customer>> GetCustomersAsync(string token);
    Task<List<Table>> GetTablesAsync();
    Task<List<Reservation>> GetReservationsAsync();
    Task AddCustomerAsync(Customer customer);
    Task AddTableAsync(Table table);
    Task MakeReservationAsync(Reservation reservation);
}
