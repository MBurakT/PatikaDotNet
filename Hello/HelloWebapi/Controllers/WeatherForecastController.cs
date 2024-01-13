using System;
using System.Collections.Generic;
using System.Linq;
using HelloWebapi.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.Logging;

namespace HelloWebapi.Controllers;

[ApiController]
[Route("[controller]s")]
public class WeatherForecastController : ControllerBase
{
    private static readonly string[] Summaries = ["Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"];

    WeatherForecast[] Weathers
    {
        get => Enumerable.Range(1, 5).Select(index => new WeatherForecast
        {
            Date = DateOnly.FromDateTime(DateTime.Now.AddDays(index)),
            TemperatureC = Random.Shared.Next(-20, 55),
            Summary = Summaries[Random.Shared.Next(Summaries.Length)]
        }).ToArray();
    }

    private readonly ILogger<WeatherForecastController> _logger;

    public WeatherForecastController(ILogger<WeatherForecastController> logger)
    {
        _logger = logger;
    }

    [HttpGet]
    public IEnumerable<WeatherForecast> GetWeatherForecasts([FromQuery] string? id)
    {
        if (string.IsNullOrEmpty(id)) return Weathers;

        if (!int.TryParse(id, out int intId) || intId < 1 || intId > 6) return [];

        return [Weathers[intId - 1]];
    }

    [HttpGet("{id:int}")]
    public WeatherForecast? GetWeatherForecastById(int id)
    {
        if (id > 0 && id < 6) return Weathers[id - 1];

        return null;
    }
}