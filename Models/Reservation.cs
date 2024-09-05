using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace RestaurantReservationSystem.Mvc.Models
{
    public class Reservation
    {
        [Required(ErrorMessage = "{0} can't be empty.")]
        [DisplayName("Reservation id")]
        public int? ReservationId { get; set; }

        [Required(ErrorMessage = "Date and Time can't be empty.")]
        public DateTime? DateTime { get; set; }

        [Required(ErrorMessage = "Customer id should be selected.")]
        public int? CustomerId { get; set; }

        [Required(ErrorMessage = "Table id should be selected.")]
        public int? TableId { get; set; }
    }
}
