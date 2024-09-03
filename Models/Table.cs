using System.ComponentModel.DataAnnotations;

namespace RestaurantReservationSystem.Mvc.Models
{
    public class Table
    {
        [Required(ErrorMessage = "Table id cant be empty!")]
        public int? TableId { get; set; }
        [Required(ErrorMessage = "Capacity cant be empty!")]
        public int? Capacity { get; set; }
        [Required(ErrorMessage = "Cost cant be empty!")]
        public int? Cost { get; set; }
        public bool IsReserved { get; set; } = false;
    }

    public class VipTable : Table
    {
        public string SpecialService { get; set; }
    }

    public class StandardTable : Table
    {
        public bool NearWindow { get; set; }
    }
}
