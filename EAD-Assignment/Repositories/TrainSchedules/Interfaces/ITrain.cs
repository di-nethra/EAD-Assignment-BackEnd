using EAD_Assignment.Models.TrainSchedules;

namespace EAD_Assignment.Repositories.TrainSchedules.Interfaces;

public interface ITrain
{
    Train CreateTrain(Train train);

    public Train GetTrainById(string trainId);

    public String DeleteTrainById(string trainId);

    public Train UpdateTrainById(string trainId, Train updatedTrain);

    public List<Train> GetAllTrains();

    public List<Train> GetTrainByStations(string departureStation, string arrivalStation);

}