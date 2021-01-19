using Contracts;
using MassTransit;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ListenerApi
{
    public class WeatherConsumer : IConsumer<WeatherForecastUpdatedList>
    {
        ILogger<WeatherForecastUpdatedList> _logger;

        public WeatherConsumer(ILogger<WeatherForecastUpdatedList> logger)
        {
            _logger = logger;
        }

        public async Task Consume(ConsumeContext<WeatherForecastUpdatedList> context)
        {
            _logger.LogInformation($"Weather forecast count: {context.Message.WeatherForecastUpdatedDays.Count}");
        }
    }
}
