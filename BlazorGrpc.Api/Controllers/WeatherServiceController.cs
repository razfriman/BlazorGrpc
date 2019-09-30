using System.Diagnostics;
using System.Threading.Tasks;
using BlazorGrpc.Protos;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace BlazorGrpc.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WeatherServiceController : ControllerBase
    {
        private readonly WeatherService.WeatherServiceClient _weatherClient;
        private readonly ILogger _logger;

        public WeatherServiceController(
            WeatherService.WeatherServiceClient weatherClient,
            ILogger<WeatherServiceController> logger
        )
        {
            _weatherClient = weatherClient;
            _logger = logger;
        }

        [HttpPost("GetWeather")]
        public async Task<GetWeatherResponse> GetWeather(GetWeatherRequest request)
        {
            _logger.LogInformation("[{Service}] [{Method}] [{TraceId}]", "gRPC API Gateway", "GetWeather",
                Activity.Current.RootId);
            return await _weatherClient.GetWeatherAsync(request);
        }
    }
}
