using BlazorGrpc.Protos;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;

namespace BlazorGrpc.Web.Server.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ConfigurationController : ControllerBase
    {
        private readonly IConfiguration _configuration;

        public ConfigurationController(IConfiguration configuration) => _configuration = configuration;

        [HttpGet]
        public WebAppConfiguration Get() =>
            new WebAppConfiguration
            {
                ApiUrl = _configuration["ApiUrl"]
            };
    }
}
