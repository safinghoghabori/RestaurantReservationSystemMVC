using System;

/// <summary>
/// Represents an exception that is thrown when an overbooking occurs in the restaurant reservation system.
/// </summary>

namespace RestaurantReservationSystem.Mvc.Exceptions
{
    public class OverBookingException : Exception
    {
        public OverBookingException(string message) : base(message) { }
    }
}
