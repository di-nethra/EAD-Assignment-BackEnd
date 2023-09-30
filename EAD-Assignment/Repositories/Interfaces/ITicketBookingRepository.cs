using EAD_Assignment.Models;

public interface ITicketBookingRepository
{
    TicketBooking CreateBooking(TicketBooking booking);

    public TicketBooking GetBookingByReservationId(string reservationId);

    public String DeleteBookingByReferenceId(string referenceId);

    public TicketBooking UpdateBookingByReservationId(string reservationId, TicketBooking updatedBooking);
    // bool CancelBooking(int bookingId);
    // List<TicketBooking> GetBookingsByReferenceId(string referenceId);
}