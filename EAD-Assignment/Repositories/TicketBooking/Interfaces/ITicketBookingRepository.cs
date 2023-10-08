using EAD_Assignment.Models;

public interface ITicketBookingRepository
{
    TicketBooking CreateBooking(TicketBooking booking);

    public TicketBooking GetBookingByReservationId(string reservationId);

    public String DeleteBookingByReferenceId(string referenceId);

    public TicketBooking UpdateBookingByReservationId(string reservationId, TicketBooking updatedBooking);

    public List<TicketBooking> GetBookingsByReferenceIdAndTrue(string referenceId,bool status);
    public List<TicketBooking> GetAllBookings();

}