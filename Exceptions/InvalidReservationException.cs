using System;

namespace RestaurantReservationSystem.Mvc.Exceptions
{
    public class InvalidReservationException : Exception
    {
        public InvalidReservationException(string message) : base(message) { }
    }
}
