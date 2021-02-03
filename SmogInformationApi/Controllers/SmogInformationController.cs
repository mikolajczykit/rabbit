using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Contracts;
using MassTransit;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace SmogInformationApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class SmogInformationController : ControllerBase
    {
        private static readonly string[] Summaries = new[]
        {
            "None", "Small", "Medium", "High", "Very high", "Dangerous"
        };

        private readonly ILogger<SmogInformationController> _logger;
        private readonly ISendEndpointProvider _sendEndpointProvider;
        private readonly IMapper _mapper;

        public SmogInformationController(ILogger<SmogInformationController> logger, ISendEndpointProvider sendEndpointProvider, IMapper mapper)
        {
            _logger = logger;
            _sendEndpointProvider = sendEndpointProvider;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IEnumerable<SmogInformation>> Get()
        {
            var rng = new Random();
            var data =  Enumerable.Range(1, 5).Select(index => new SmogInformation
            {
                Date = DateTime.Now.AddDays(index),
                Summary = Summaries[rng.Next(Summaries.Length)]
            }).ToArray();

            var endpoint = await _sendEndpointProvider.GetSendEndpoint(new Uri("queue:test-queue"));
            var message = new SmogInformationUpdatedList(data.Select(x => _mapper.Map<SmogInformationUpdated>(x)).ToList());

            await endpoint.Send<SmogInformationUpdatedList>(message);

            return data;
        }
    }
}
