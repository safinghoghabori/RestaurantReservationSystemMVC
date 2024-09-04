using System.ComponentModel.DataAnnotations;

/// <summary>
    /// The Table class represents a table in the restaurant with properties for ID, capacity, cost, and reservation status. 
    /// The VipTable class inherits from Table and adds a special service property, 
    /// while the StandardTable class also inherits from Table and includes a property indicating if it is near a window.
/// </summary>

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
