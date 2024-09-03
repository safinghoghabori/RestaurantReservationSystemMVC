using RestaurantReservationSystem.Mvc.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

public interface IRestaurantService
{
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
    Task MakeReservationAsync(Reservation reservation);
    Task DeleteCustomerAsync(int id, string token);
}

