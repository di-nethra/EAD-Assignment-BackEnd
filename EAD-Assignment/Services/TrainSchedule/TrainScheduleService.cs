using EAD_Assignment.Models.TrainSchedules;
using EAD_Assignment.Repositories.TrainSchedules.Interfaces;
using MongoDB.Bson;

namespace EAD_Assignment.Services.TrainSchedule;

public class TrainScheduleService
{
    private readonly ITrain _trainRepository;

    public TrainScheduleService(ITrain trainRepository)
    {
        _trainRepository = trainRepository;
    }

    public Train CreateTrain(Train train)
    {
        var createTrain = _trainRepository.CreateTrain(train);
        return createTrain;
    }

    public String DeleteTrainById(ObjectId trainId)
    {
        var deleteStatus = _trainRepository.DeleteTrainById(trainId);
        return deleteStatus;
    }

    public Train UpdateTrainById(ObjectId trainId, Train updatedTrain)
    {
        var updateTrain = _trainRepository.UpdateTrainById(trainId, updatedTrain);
        return updateTrain;
    }

    public List<Train> GetAllTrains()
    {
        try
        {
            var trains = _trainRepository.GetAllTrains();
            return trains;
        }
        catch (Exception e)
        {
            throw e;
        }
    }

    public List<Train> GetTrainByStations(string departureStation, string arrivalStation)
    {
        try
        {
            var avlTrains = _trainRepository.GetTrainByStations(departureStation, arrivalStation);
            return avlTrains;
        }
        catch (Exception e)
        {
            throw e;
        }
    }
    
}