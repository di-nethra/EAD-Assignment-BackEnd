using EAD_Assignment.Models.TrainSchedules;
using EAD_Assignment.Repositories.TrainSchedules.Interfaces;
using MongoDB.Bson;
using MongoDB.Driver;

namespace EAD_Assignment.Repositories.TrainSchedules;

public class TrainScheduleRepository : ITrain
{
    private readonly IMongoCollection<Train> _trains;

    public TrainScheduleRepository(IConfiguration configuration)
    {
        var connectionString =  configuration["MongoDBSettings:ConnectionString"];
        var databaseName = configuration["MongoDBSettings:DatabaseName"];
        var client = new MongoClient(connectionString);
        var database = client.GetDatabase(databaseName);
        _trains = database.GetCollection<Train>("Trains");
    }

    public Train CreateTrain(Train train)
    {
        try
        {
            _trains.InsertOne(train);
            return train;
        }
        catch (Exception e)
        {
            throw e;
        }
    }

    public Train GetTrainById(ObjectId trainId)
    {
        try
        {
            var train = _trains.Find(t => t.Id == trainId).FirstOrDefault();
            return train;
        }
        catch (Exception e)
        {
            throw e;
        }
    }

    public String DeleteTrainById(ObjectId trainId)
    {
        try
        {
            var filter = Builders<Train>.Filter.Eq(t => t.Id, trainId);
            _trains.DeleteOne(filter);
            return "Successfully Deleted";
        }
        catch (Exception e)
        {
            throw e;
        }
    }

    public Train UpdateTrainById(ObjectId trainId, Train updatedTrain)
    {
        try
        {
            var filter = Builders<Train>.Filter.Eq(t => t.Id, trainId);
            var update = Builders<Train>.Update
                .Set(t => t.Id, updatedTrain.Id)
                .Set(t => t.TrainName, updatedTrain.TrainName)
                .Set(t => t.TrainNumber, updatedTrain.TrainNumber)
                .Set(t => t.DepartureStation, updatedTrain.DepartureStation)
                .Set(t => t.ArrivalStation, updatedTrain.ArrivalStation)
                .Set(t => t.DepartureTime, updatedTrain.DepartureTime)
                .Set(t => t.ArrivalTime, updatedTrain.ArrivalTime);


            var result = _trains.UpdateOne(filter, update);
            if (result.ModifiedCount > 0)
            {
                var updatedRecord = _trains.Find(filter).FirstOrDefault();
                return updatedRecord;
            }
            else
            {
                return null;
            }
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }

    public List<Train> GetAllTrains()
    {
        try
        {
            var trains = _trains.Find(_ => true).ToList();
            return trains;
        }
        catch (Exception e)
        {
            throw e;
        }
    }

    public List<Train> GetTrainByStations(string departureStation, string arrivalStation)
    {
        return _trains.Find(t => t.DepartureStation.Equals(departureStation, StringComparison.Ordinal)
                                                && t.ArrivalStation.Equals(arrivalStation, StringComparison.Ordinal))
            .ToList();
    }
}