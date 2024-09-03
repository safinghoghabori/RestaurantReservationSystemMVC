using RestaurantReservationSystem.Mvc.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

public interface IRestaurantService
{
    Task<List<Customer>> GetCustomersAsync(string token);
    Task<Customer> GetCustomerById(int id, string token); 
    Task UpdateCustomerAsync(Customer customer, string token); 
    Task<List<Table>> GetTablesAsync();
    Task<List<Reservation>> GetReservationsAsync();
    Task AddCustomerAsync(Customer customer, string token); 
    Task AddTableAsync(Table table);
    Task MakeReservationAsync(Reservation reservation);
    Task DeleteCustomerAsync(int id, string token);
}

