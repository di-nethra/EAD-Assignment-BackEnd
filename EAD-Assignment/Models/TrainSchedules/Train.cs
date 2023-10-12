using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace EAD_Assignment.Models.TrainSchedules;

public class Train
{
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)] 
    public ObjectId Id { get; set; }
    public string TrainNumber { get; set; }
    public string TrainName { get; set; }
    public string DepartureTime { get; set; }
    public string ArrivalTime { get; set; }
    public string DepartureStation { get; set; }
    public string ArrivalStation { get; set; }
}