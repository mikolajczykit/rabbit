using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
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
        private readonly IMapper _mapper;

        private static readonly string[] Summaries = new[]
        {
            "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
        };

        private readonly ILogger<WeatherForecastController> _logger;

        private readonly ISendEndpointProvider _sendEndpointProvider;

        public WeatherForecastController(ILogger<WeatherForecastController> logger,
            ISendEndpointProvider sendEndpointProvider, 
            IMapper mapper)
        {
            _logger = logger;
            _sendEndpointProvider = sendEndpointProvider;
            _mapper = mapper;
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
            var message = new WeatherForecastUpdatedList(data.Select(x => _mapper.Map<WeatherForecastUpdated>(x)).ToList());
            await endpoint.Send<WeatherForecastUpdatedList>(message);

            return data;
        }
    }
}
