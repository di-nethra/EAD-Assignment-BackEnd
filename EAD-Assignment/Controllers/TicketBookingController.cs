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
    private readonly TicketBookingService ticketBookingService;

    public TicketBookingController(TicketBookingService ticketBookingService)
    {
        this.ticketBookingService = ticketBookingService;
    }

    [HttpPost]
    public IActionResult CreateBooking([FromBody] TicketBooking booking)
    {
        var createdBooking = ticketBookingService.CreateBooking(booking);
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
        var record = ticketBookingService.GetReservationById(reservationId);

        if (record == null)
        {
            return NotFound(); 
        }

        return Ok(record);
    }
 
    
}


