using Contracts;
using MassTransit;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ListenerApi
{
    public class WeatherConsumer : IConsumer<WeatherForecastUpdated>
    {
        ILogger<WeatherForecastUpdated> _logger;

        public WeatherConsumer(ILogger<WeatherForecastUpdated> logger)
        {
            _logger = logger;
        }

        public async Task Consume(ConsumeContext<WeatherForecastUpdated> context)
        {
            _logger.LogInformation($"Weather summary: {context.Message.Summary}");
        }
    }
}
