using WeatherService.Models;
using WeatherService.Services;
using Microsoft.Extensions.Options;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.Configure<WeatherServiceSettings>(builder.Configuration.GetSection("OpenWeather"));
builder.Services.AddHttpClient<WeatherDataService>();

// Register the WeatherDataService with DI to resolve dependencies
builder.Services.AddScoped<IWeatherDataService, WeatherDataService>();

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

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
