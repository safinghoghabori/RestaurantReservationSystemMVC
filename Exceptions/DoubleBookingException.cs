using System;

namespace RestaurantReservationSystem.Mvc.Exceptions
{
    public class DoubleBookingException : Exception
    {
        public DoubleBookingException(string message) : base(message) { }
    }
}
