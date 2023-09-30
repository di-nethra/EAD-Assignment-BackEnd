
using EAD_Assignment.Models;

namespace EAD_Assignment.Services;

public class TicketBookingService
{
    private readonly ITicketBookingRepository _ticketBookingRepository;

    public TicketBookingService(ITicketBookingRepository ticketBookingRepository)
    {
        _ticketBookingRepository = ticketBookingRepository;
    }
    public TicketBooking CreateBooking(TicketBooking booking)
    {
        // Implement validation logic here if needed.
        var createdBooking = _ticketBookingRepository.CreateBooking(booking);
        return createdBooking;
    }

    // public TicketBooking CreateBooking(TicketBooking booking)
    // {
    //     // Implement validation logic here.
    //
    //     // Example: Check if the reservation date is within 30 days from the booking date.
    //     var dateDifference = (booking.ReservationDate - DateTime.Now).Days;
    //
    //     if (dateDifference <= 30)
    //     {
    //         // Call the repository to create the booking.
    //         var createdBooking = _ticketBookingRepository.CreateBooking(booking);
    //
    //         // Return the created booking.
    //         return createdBooking;
    //     }
    //     else
    //     {
    //         throw new ArgumentException("Reservation date must be within 30 days from the booking date.");
    //     }
    // }

    //
    // public TicketBooking UpdateBooking(int bookingId, TicketBooking updatedBooking)
    // {
    //     // Implement validation logic and call the repository to update the booking.
    //     // Return the updated booking.
    // }
    //
    // public bool CancelBooking(int bookingId)
    // {
    //     // Implement cancellation logic and call the repository to cancel the booking.
    //     // Return true if canceled, false otherwise.
    // }
}