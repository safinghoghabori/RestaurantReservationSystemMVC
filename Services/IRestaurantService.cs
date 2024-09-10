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
    Task DeleteCustomerAsync(int id, string token);

    // Reservation-related methods
    Task<List<Reservation>> GetReservationsAsync(string token);
    Task<Reservation> GetReservationById(int id, string token);
    Task AddReservationAsync(Reservation reservation, string token);
    Task UpdateReservationAsync(Reservation reservation, string token);
    Task DeleteReservationAsync(int id, string token);

    Task<List<Booking>> GetBookingsAsync(string token);
    Task<Booking> GetBookingById(int id, string token);
    Task<Booking> GetBookingByEmail(string email, string token);
    Task AddBookingAsync(Booking booking, string token);
    Task UpdateBookingAsync(Booking booking, string token);
    Task DeleteBookingAsync(int id, string token);
}


