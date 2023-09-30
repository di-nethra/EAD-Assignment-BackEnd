using EAD_Assignment.Models;

public interface ITicketBookingRepository
{
    TicketBooking CreateBooking(TicketBooking booking);
    // TicketBooking UpdateBooking(int bookingId, TicketBooking updatedBooking);
    // bool CancelBooking(int bookingId);
    // TicketBooking GetBookingById(int bookingId);
    // List<TicketBooking> GetBookingsByReferenceId(string referenceId);
}