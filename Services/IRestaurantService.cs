using RestaurantReservationSystem.Mvc.Models;

public interface IRestaurantService
{
    Task<List<Customer>> GetCustomersAsync(string token);
    Task AddCustomerAsync(Customer customer);
    Task<List<Table>> GetTablesAsync(string token);
    Task AddTableAsync(string token, Table table);
    Task<Table> GetTableById(int id, string token);
    Task UpdateTable(Table table, string token);
    Task DeleteTable(int id, string token);
    Task<List<Reservation>> GetReservationsAsync();
    Task MakeReservationAsync(Reservation reservation);
}
