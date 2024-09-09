using System.ComponentModel.DataAnnotations;

namespace RestaurantReservationSystem.Mvc.Models
{
    public class Customer
    {
        public int CustomerId { get; set; }

        [Required(ErrorMessage = "Name is required")]
        [StringLength(30, MinimumLength = 3, ErrorMessage = "Name can't be longer than 300 characters")]
        public string Name { get; set; }
        [Required(ErrorMessage = "Phone number should be provided")]
        [RegularExpression(@"^[6-9]\d{9}$", ErrorMessage = "Please enter a valid 10-digit phone number starting with 6, 7, 8, or 9.")]
        public string PhoneNumber { get; set; }
        [Required(ErrorMessage = "Age is required")]
        [Range(1, 100, ErrorMessage = "Age must be between 1 and 100")]
        public int Age { get; set; }
        public string Gender { get; set; }
    }
}
