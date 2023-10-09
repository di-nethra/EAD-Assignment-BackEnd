using EAD_Assignment.Repositories;
using EAD_Assignment.Repositories.UserRepository;
using EAD_Assignment.Repositories.UserRepository.Interfaces;
using EAD_Assignment.Services;
using EAD_Assignment.Services.TokenService;
using EAD_Assignment.Services.TokenService.Interfaces;
using EAD_Assignment.Services.UserServices;
using EAD_Assignment.Services.UserServices.Interfaces;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

builder.Configuration.AddJsonFile("appsettings.json");

// Add services to the container.
builder.Services.AddControllers();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Add your custom services and repositories
builder.Services.AddTransient<ITicketBookingRepository, TicketBookingRepository>();
builder.Services.AddScoped<TicketBookingService>();
builder.Services.AddScoped<ITokenService, TokenService>();
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IUserService, UserService>();

// Configure authentication
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,         // Validate the JWT issuer
            ValidateAudience = false,       // Validate the audience
            ValidateLifetime = false,       // Validate token lifetime
            ValidateIssuerSigningKey = true, // Validate the signing key

            // Replace with your actual configuration values
            ValidIssuer = builder.Configuration["Jwt:Issuer"],
            ValidAudience = builder.Configuration["Jwt:Audience"],
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Jwt:SecretKey"]))
        };
    });

// Configure authorization policies
builder.Services.AddAuthorization(options =>
{
    options.AddPolicy("BackOfficeAgent", policy =>
    {
        policy.RequireAuthenticatedUser();
        policy.RequireRole("BackOfficeAgent");
    });

    options.AddPolicy("TravelAgent", policy =>
    {
        policy.RequireAuthenticatedUser();
        policy.RequireRole("TravelAgent");
    });

    options.AddPolicy("Traveller", policy =>
    {
        policy.RequireAuthenticatedUser();
        policy.RequireRole("Traveller");
    });
});

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