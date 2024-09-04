/// <summary>
    /// Represents an error view model that contains information about the request ID.
    /// Properties:
    /// - RequestId: Gets or sets the request ID associated with the error.
    /// - ShowRequestId: Gets a value indicating whether the request ID should be shown.
/// </summary>

namespace RestaurantReservationSystemMVC.Models;

public class ErrorViewModel
{
    public string? RequestId { get; set; }

    public bool ShowRequestId => !string.IsNullOrEmpty(RequestId);
}
