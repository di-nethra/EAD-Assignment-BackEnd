using System;

namespace EAD_Assignment.Models
{
    public class UpdateReservationRequest
    {
        public DateTime ReservationDate { get; set; }
        public bool Status { get; set; }
    }
}