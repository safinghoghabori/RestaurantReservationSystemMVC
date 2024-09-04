using System;

/// <summary>
    /// The InvalidReservationException class is a custom exception used to handle scenarios where an invalid reservation occurs in the restaurant reservation system.
    /// It inherits from the base Exception class and provides a constructor that accepts a custom error message.
/// </summary>

namespace RestaurantReservationSystem.Mvc.Exceptions
{
    public class InvalidReservationException : Exception
    {
        public InvalidReservationException(string message) : base(message) { }
    }
}
