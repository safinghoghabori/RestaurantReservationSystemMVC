using RestaurantReservationSystem.Mvc.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

/// <summary>
    /// Defines the contract for restaurant-related operations, including customer, table, and reservation management.
    /// Methods:
    /// - GetCustomersAsync: Retrieves a list of customers asynchronously.
    /// - GetCustomerById: Retrieves a customer by their ID asynchronously.
    /// - UpdateCustomerAsync: Updates customer information asynchronously.
    /// - AddCustomerAsync: Adds a new customer asynchronously.
    /// - GetTablesAsync: Retrieves a list of tables asynchronously.
    /// - AddTableAsync: Adds a new table asynchronously.
    /// - GetTableById: Retrieves a table by its ID asynchronously.
    /// - UpdateTable: Updates table information asynchronously.
    /// - DeleteTable: Deletes a table by its ID asynchronously.
    /// - DeleteCustomerAsync: Deletes a customer by their ID asynchronously.
    /// - GetReservationsAsync: Retrieves a list of reservations asynchronously.
    /// - GetReservationById: Retrieves a reservation by its ID asynchronously.
    /// - AddReservationAsync: Adds a new reservation asynchronously.
    /// - UpdateReservationAsync: Updates reservation information asynchronously.
    /// - DeleteReservationAsync: Deletes a reservation by its ID asynchronously.
/// </summary>

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
    Task DeleteCustomerAsync(int id, string token);

    // Reservation-related methods
    Task<List<Reservation>> GetReservationsAsync(string token);
    Task<Reservation> GetReservationById(int id, string token);
    Task AddReservationAsync(Reservation reservation, string token);
    Task UpdateReservationAsync(Reservation reservation, string token);
    Task DeleteReservationAsync(int id, string token);
}


