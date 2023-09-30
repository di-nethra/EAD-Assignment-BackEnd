using EAD_Assignment.Models;
using MongoDB.Driver;
using Microsoft.Extensions.Configuration;
namespace EAD_Assignment.Repositories;

public class TicketBookingRepository : ITicketBookingRepository
{
    private readonly IMongoCollection<TicketBooking> _bookingCollection;

    public TicketBookingRepository(IConfiguration configuration)
    {
       
        var connectionString =  configuration["MongoDBSettings:ConnectionString"];
        var databaseName = configuration["MongoDBSettings:DatabaseName"];
        var client = new MongoClient(connectionString);
        var database = client.GetDatabase(databaseName);
        _bookingCollection = database.GetCollection<TicketBooking>("bookings");
    }

    public TicketBooking CreateBooking(TicketBooking booking)
    {
        try
        {
            _bookingCollection.InsertOne(booking);
            return booking;
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }
    
    public TicketBooking GetBookingByReservationId(string reservationId)
    {
        try
        {
            var booking = _bookingCollection.Find(b => b.ReferenceId == reservationId).FirstOrDefault();
            return booking;
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

}