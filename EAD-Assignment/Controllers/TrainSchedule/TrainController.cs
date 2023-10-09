using EAD_Assignment.Models.TrainSchedules;
using EAD_Assignment.Services.TrainSchedule;
using Microsoft.AspNetCore.Mvc;

namespace EAD_Assignment.Controllers.TrainSchedule;

[Route("api/train")]
[ApiController]
public class TrainController : ControllerBase
{
    private readonly TrainScheduleService _trainScheduleService;

    public TrainController(TrainScheduleService trainScheduleService)
    {
        _trainScheduleService = trainScheduleService;
    }

    [HttpPost]
    public IActionResult CreateTrain(Train train)
    {
        var createTrain = _trainScheduleService.CreateTrain(train);
        return Ok(createTrain);
    }

    [HttpGet]
    public IActionResult GetAllTrain()
    {
        var allTrains = _trainScheduleService.GetAllTrains();
        return Ok(allTrains);
    }

    [HttpGet("search")]
    public IActionResult GetTrainByStations([FromQuery] string departureStation, [FromQuery] string arrivalStation)
    {
        var avalTrains = _trainScheduleService.GetTrainByStations(departureStation, arrivalStation);
        return Ok(avalTrains);
    }
   
}