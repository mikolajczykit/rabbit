using Contracts;
using MassTransit;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ListenerApi
{
    public class SmogConsumer : IConsumer<SmogInformationUpdatedList>
    {
        ILogger<SmogInformationUpdatedList> _logger;

        public SmogConsumer(ILogger<SmogInformationUpdatedList> logger)
        {
            _logger = logger;
        }

        public async Task Consume(ConsumeContext<SmogInformationUpdatedList> context)
        {
            var smogInformation = $"Smog information count: {string.Join(", ", context.Message.SmogInformationUpdatedDays.Select(x => $"{x.Date.ToShortDateString()} - {x.Summary}"))}";
            _logger.LogInformation(smogInformation);
        }
    }
}
