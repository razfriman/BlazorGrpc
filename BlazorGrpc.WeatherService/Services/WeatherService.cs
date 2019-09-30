using System;
using System.Diagnostics;
using System.Threading.Tasks;
using Grpc.Core;
using Microsoft.Extensions.Logging;
using BlazorGrpc.Protos;

namespace BlazorGrpc.WeatherService.Services
{
    public class WeatherService : Protos.WeatherService.WeatherServiceBase
    {
        private readonly ILogger<WeatherService> _logger;

        public WeatherService(ILogger<WeatherService> logger) => _logger = logger;

        public override Task<GetWeatherResponse> GetWeather(GetWeatherRequest request, ServerCallContext context)
        {
            _logger.LogInformation("[{Service}] [{Method}] [{TraceId}]", "gRPC Weather Service", "GetWeather",
                Activity.Current.RootId);

            var rand = new Random();
            var response = new GetWeatherResponse();

            for (var i = 0; i < 3; i++)
            {
                response.Weathers.Add(new Weather
                {
                    TemperatureC = rand.Next(1, 100),
                    TemperatureF = rand.Next(1, 100),
                    Date = DateTime.Now.ToShortDateString(),
                    Summary = rand.NextDouble() >= 0.5 ? "Sunny" : "Cloudy"
                });
            }

            return Task.FromResult(response);
        }
    }
}