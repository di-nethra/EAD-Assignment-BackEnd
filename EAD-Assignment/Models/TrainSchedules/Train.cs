using MongoDB.Bson;

namespace EAD_Assignment.Models.TrainSchedules;

public class Train
{
    public string Id { get; set; }
    public string TrainNumber { get; set; }
    public string TrainName { get; set; }
    public DateTime DepartureTime { get; set; }
    public DateTime ArrivalTime { get; set; }
    public string DepartureStation { get; set; }
    public string ArrivalStation { get; set; }
}