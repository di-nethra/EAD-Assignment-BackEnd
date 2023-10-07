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
    public String DeleteBookingByReferenceId(string referenceId)
    {
        try
        {
            var filter = Builders<TicketBooking>.Filter.Eq(b => b.ReferenceId, referenceId);
            _bookingCollection.DeleteOne(filter);
            return "Successfully Deleted";
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }
    public TicketBooking UpdateBookingByReservationId(string reservationId, TicketBooking updatedBooking)
    {
        try
        {
            var filter = Builders<TicketBooking>.Filter.Eq(b => b.ReferenceId, reservationId);
            var update = Builders<TicketBooking>.Update
                .Set(b => b.Status, updatedBooking.Status)
                .Set(b => b.BookingDate, updatedBooking.BookingDate)
                .Set(b => b.ReservationDate, updatedBooking.ReservationDate)
                .Set(b => b.TrainId, updatedBooking.TrainId);

            var result = _bookingCollection.UpdateOne(filter, update);

            if (result.ModifiedCount > 0)
            {
                var updatedRecord = _bookingCollection.Find(filter).FirstOrDefault();
                return updatedRecord;
            }
            else
            {
                return null; 
            }
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }
    
    
    public List<TicketBooking> GetBookingsByReferenceIdAndTrue(string referenceId,bool status)
    {
        try
        {
            var filter = Builders<TicketBooking>.Filter.Eq(b => b.ReferenceId, referenceId) &
                         Builders<TicketBooking>.Filter.Eq(b => b.Status, status);

            var bookings = _bookingCollection.Find(filter).ToList();
            return bookings;
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

}