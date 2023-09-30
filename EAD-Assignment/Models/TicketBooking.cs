using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace EAD_Assignment.Models;
public class TicketBooking
{
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    public string Id { get; set; } 
    public string ReferenceId { get; set; }
    public string TravellerId { get; set; }
    public string TrainId { get; set; }
    public bool Status { get; set; }
    public DateTime BookingDate { get; set; }
    public DateTime ReservationDate { get; set; }
}