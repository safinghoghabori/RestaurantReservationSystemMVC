namespace RestaurantReservationSystem.Mvc.Models
{
    public class Reservation
    {
        public int ReservationId { get; set; }
        public DateTime DateTime { get; set; }
        public int CustomerId { get; set; }
        public int TableId { get; set; }
    }
}
