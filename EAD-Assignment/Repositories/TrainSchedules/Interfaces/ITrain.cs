using EAD_Assignment.Models.TrainSchedules;
using MongoDB.Bson;

namespace EAD_Assignment.Repositories.TrainSchedules.Interfaces;

public interface ITrain
{
    Train CreateTrain(Train train);

    public Train GetTrainById(ObjectId trainId);

    public String DeleteTrainById(ObjectId trainId);

    public Train UpdateTrainById(ObjectId trainId, Train updatedTrain);

    public List<Train> GetAllTrains();

    public List<Train> GetTrainByStations(string departureStation, string arrivalStation);

}