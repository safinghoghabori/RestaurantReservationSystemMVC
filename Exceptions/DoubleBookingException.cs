using System;

/// <summary>
    /// The DoubleBookingException class is a custom exception used to handle scenarios where a double booking occurs in the restaurant reservation system.
    /// It inherits from the base Exception class and provides a constructor that accepts a custom error message.
/// </summary>

namespace RestaurantReservationSystem.Mvc.Exceptions
{
    public class DoubleBookingException : Exception
    {
        public DoubleBookingException(string message) : base(message) { }
    }
}
