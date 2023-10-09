using EAD_Assignment.Models.TrainSchedules;
using EAD_Assignment.Repositories;
using EAD_Assignment.Repositories.TrainSchedules;
using EAD_Assignment.Repositories.TrainSchedules.Interfaces;
using EAD_Assignment.Services;
using EAD_Assignment.Services.TrainSchedule;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
// builder.Services.AddTransient<ITicketBookingService, TicketBookingService>();
builder.Services.AddTransient<ITicketBookingRepository, TicketBookingRepository>();
builder.Services.AddScoped<TicketBookingService>();

builder.Services.AddTransient<ITrain, TrainScheduleRepository>();
builder.Services.AddScoped<TrainScheduleService>();
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
//app.Run("http://192.168.1.31:5227");