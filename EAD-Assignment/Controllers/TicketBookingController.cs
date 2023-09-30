using EAD_Assignment.Models;
using EAD_Assignment.Services;
using Microsoft.AspNetCore.Mvc;

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
    [HttpGet("{id}")]
    public IActionResult GetBookingById(int id)
    {
        // Implement the logic to retrieve a booking by its ID here.
        return null;
    }
    
}


