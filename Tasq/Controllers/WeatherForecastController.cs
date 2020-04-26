using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LoggerService;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace Tasq.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastController : ControllerBase
    {

        private ILoggerManager _logger;

        public WeatherForecastController(ILoggerManager logger)
        {
            _logger = logger;
        }

        [HttpGet]
        public IEnumerable<string> Get()
        {
            _logger.LogInfo("Инфо мессадж.");
            _logger.LogDebug("Дебаг мессадж.");
            _logger.LogWarn("Варн мессадж.");
            _logger.LogError("Еррор мессадж.");

            return new string[] { "value1", "value2" };
        }
    }
}
