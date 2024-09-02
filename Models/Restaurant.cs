using System.Linq;
using RestaurantReservationSystem.Api.Exceptions;

namespace RestaurantReservationSystem.Api.Models
{
    public class Restaurant
    {
        public string Name { get; set; }
        public List<Customer> Customers { get; set; } = new List<Customer>();
        public List<Table> Tables { get; set; } = new List<Table>();
        public List<Reservation> Reservations { get; set; } = new List<Reservation>();

        public void AddCustomer(Customer customer) => Customers.Add(customer);

        public void AddTable(Table table) => Tables.Add(table);

        public void AddReservation(Reservation reservation)
        {
            if (reservation.Table.IsReserved)
            {
                throw new InvalidReservationException("Table is already reserved.");
            }

            var existingReservations = Reservations.Where(r => r.Table.TableId == reservation.Table.TableId).ToList();

            if (existingReservations.Any(r => r.DateTime == reservation.DateTime))
            {
                throw new DoubleBookingException("A reservation already exists for this table at the same time.");
            }

            if (existingReservations.Count >= reservation.Table.Capacity)
            {
                throw new OverBookingException("Cannot book this table. Overbooking detected.");
            }

            reservation.Table.IsReserved = true;
            Reservations.Add(reservation);
        }

        public void UpdateReservation(int reservationId, Reservation updatedReservation)
        {
            var reservation = Reservations.FirstOrDefault(r => r.ReservationId == reservationId);
            if (reservation == null)
            {
                throw new InvalidReservationException("Reservation not found.");
            }

            reservation.DateTime = updatedReservation.DateTime;
            reservation.Customer = updatedReservation.Customer;
            reservation.Table = updatedReservation.Table;

            AddReservation(reservation);
        }

        public void CancelReservation(int reservationId)
        {
            var reservation = Reservations.FirstOrDefault(r => r.ReservationId == reservationId);
            if (reservation == null)
            {
                throw new InvalidReservationException("Reservation not found.");
            }

            reservation.Table.IsReserved = false;
            Reservations.Remove(reservation);
        }
    }
}
