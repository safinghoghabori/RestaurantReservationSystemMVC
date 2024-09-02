using System;

namespace RestaurantReservationSystem.Mvc.Exceptions
{
    public class OverBookingException : Exception
    {
        public OverBookingException(string message) : base(message) { }
    }
}
