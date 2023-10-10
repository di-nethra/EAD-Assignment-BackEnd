
using EAD_Assignment.Models;
using Microsoft.AspNetCore.Http.HttpResults;

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
    public TicketBooking  GetReservationById(string referenceId)
    {
        var record = _ticketBookingRepository.GetBookingByReservationId(referenceId);
        return record;
    }
    
    public String  DeleteBookingByReferenceId(string referenceId)
    {
       var deleteStatus= _ticketBookingRepository.DeleteBookingByReferenceId(referenceId);
       return deleteStatus;
    }

    public TicketBooking UpdateBookingByReservationId(string reservationId, TicketBooking updatedBooking)
    {
        // var dateDifference = (updatedBooking.ReservationDate - DateTime.Now).Days;

        // if (dateDifference >= 5)
        // {
                var updatedReservation = _ticketBookingRepository.UpdateBookingByReservationId(reservationId, updatedBooking);

                return updatedReservation;
        // }
        // else
        // {
        //     throw new ArgumentException("Updated reservation date must be within 5 days from the booking date.");
        // }
    }
    
    
    
    public List<TicketBooking> GetBookingsByReferenceIdAndStatus(string referenceId,bool status)
    {
        try
        {
            var bookings = _ticketBookingRepository.GetBookingsByReferenceIdAndTrue(referenceId,status);
            return bookings;
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    public List<TicketBooking> GetAllBookings()
    {
        try
        {
            var bookings = _ticketBookingRepository.GetAllBookings();
            return bookings;
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }
    
    
    public TicketBooking UpdateReservationDateAndStatus(string reservationId, DateTime reservationDate, bool status)
    {
        try
        {
            var existingBooking = _ticketBookingRepository.GetBookingByReservationId(reservationId);

            if (existingBooking != null)
            {
                existingBooking.ReservationDate = reservationDate;
                existingBooking.Status = status;
                existingBooking.BookingDate = DateTime.Now; // Set BookingDate to current date

                var updatedBooking = _ticketBookingRepository.UpdateBookingByReservationId(reservationId, existingBooking);
            
                if (updatedBooking != null)
                {
                    // You can perform additional processing or validation here if needed.
                    return updatedBooking;
                }
            }

            return null; // Handle the case where the booking was not found or not updated.
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }


}