using RestaurantReservationSystem.Mvc.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

public interface IRestaurantService
{
    // Customer-related methods
    Task<List<Customer>> GetCustomersAsync(string token);
    Task<Customer> GetCustomerById(int id, string token);
    Task UpdateCustomerAsync(Customer customer, string token);
    Task AddCustomerAsync(Customer customer, string token);
    Task<List<Table>> GetTablesAsync(string token);
    Task AddTableAsync(string token, Table table);
    Task<Table> GetTableById(int id, string token);
    Task UpdateTable(Table table, string token);
    Task DeleteTable(int id, string token);
    Task<List<Reservation>> GetReservationsAsync();
    Task DeleteCustomerAsync(int id, string token);

    // Table-related methods
    Task<List<Table>> GetTablesAsync();
    Task AddTableAsync(Table table);

    // Reservation-related methods
    Task<List<Reservation>> GetReservationsAsync(string token);
    Task<Reservation> GetReservationById(int id, string token);
    Task AddReservationAsync(Reservation reservation, string token);
    Task UpdateReservationAsync(Reservation reservation, string token);
    Task DeleteReservationAsync(int id, string token);
}


