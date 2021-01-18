using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Contracts;
using MassTransit;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace WeatherForecastApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastController : ControllerBase
    {
        private static readonly string[] Summaries = new[]
        {
            "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
        };

        private readonly ILogger<WeatherForecastController> _logger;

        private readonly ISendEndpointProvider _sendEndpointProvider;

        public WeatherForecastController(ILogger<WeatherForecastController> logger, ISendEndpointProvider sendEndpointProvider)
        {
            _logger = logger;
            _sendEndpointProvider = sendEndpointProvider;
        }

        [HttpGet]
        public async Task<IEnumerable<WeatherForecast>> Get()
        {
            var rng = new Random();
            IEnumerable<WeatherForecast> data = Enumerable.Range(1, 5).Select(index => new WeatherForecast
            {
                Date = DateTime.Now.AddDays(index),
                TemperatureC = rng.Next(-20, 55),
                Summary = Summaries[rng.Next(Summaries.Length)]
            })
            .ToArray();

            var endpoint = await _sendEndpointProvider.GetSendEndpoint(new Uri("queue:test-queue"));

            await endpoint.Send<WeatherForecastUpdated>(new
            {
                Date = DateTime.Now,
                TemperatureC = 12,
                TemperatureF = 18,
                Summary = "Rather rainy weather!"
            });


            //_publishEndpoint..Publish<WeatherForecastUpdated>(new 
            //{
            //    Date = DateTime.Now,
            //    TemperatureC = 12,
            //    TemperatureF = 18,
            //    Summary = "Rather rainy weather!"
            //});

            return data;
        }
    }
}
