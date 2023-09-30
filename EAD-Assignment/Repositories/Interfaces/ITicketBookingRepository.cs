using EAD_Assignment.Models;

public interface ITicketBookingRepository
{
    TicketBooking CreateBooking(TicketBooking booking);

    public TicketBooking GetBookingByReservationId(string reservationId);
    // TicketBooking UpdateBooking(int bookingId, TicketBooking updatedBooking);
    // bool CancelBooking(int bookingId);
    // TicketBooking GetBookingById(int bookingId);
    // List<TicketBooking> GetBookingsByReferenceId(string referenceId);
}