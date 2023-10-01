using EAD_Assignment.Models;
using EAD_Assignment.Services;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Bson;

namespace EAD_Assignment.Controllers;

[Route("api/ticketbookings")]
[ApiController]
public class TicketBookingController : ControllerBase
{
    // private TicketBookingService ticketBookingService;
    private readonly TicketBookingService _ticketBookingService;

    public TicketBookingController(TicketBookingService ticketBookingService)
    {
        _ticketBookingService = ticketBookingService;
    }

    [HttpPost]
    public IActionResult CreateBooking([FromBody] TicketBooking booking)
    {
        var createdBooking = _ticketBookingService.CreateBooking(booking);
        return Ok(createdBooking);
        // try
        // {
        //     var createdBooking = ticketBookingService.CreateBooking(booking);
        //     return CreatedAtAction(nameof(GetBookingById), new { id = createdBooking.BookingId }, createdBooking);
        // }
        // catch (Exception ex)
        // {
        //     return BadRequest(ex.Message);
        // }
    }
    
    [HttpGet("{reservationId}")]
    public IActionResult GetReservationById(string reservationId)
    {
        var record = _ticketBookingService.GetReservationById(reservationId);

        if (record == null)
        {
            return NotFound(); 
        }

        return Ok(record);
    }
 
    [HttpDelete("{reservationId}")]
    public IActionResult DeleteReservationById(string reservationId)
    {
        try
        {
            var deleteRes = _ticketBookingService.DeleteBookingByReferenceId(reservationId);
            return Ok(deleteRes); 
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }
    
    [HttpPut("{reservationId}")]
    public IActionResult UpdateReservationById(string reservationId, [FromBody] TicketBooking updatedBooking)
    {
        try
        {
            var updatedRecord = _ticketBookingService.UpdateBookingByReservationId(reservationId, updatedBooking);
        
            if (updatedRecord == null)
            {
                return NotFound();
            }

            return Ok(updatedRecord);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

}


