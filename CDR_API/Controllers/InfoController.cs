using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace CDR_API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class InfoController : ControllerBase
    {
        private static readonly string[] Summaries = new[]
        {
            "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
        };

        private readonly ILogger<InfoController> _logger;

        public InfoController(ILogger<InfoController> logger)
        {
            _logger = logger;
        }

        [HttpGet]
        public string Get()
        {
            return "to hit the endpoints you can send curl commands via command line with the sln running. Please note you will need to update the example url to match your own. Example commands are as follows." + Environment.NewLine + "Upload: curl https://localhost:44356/write/upload" + Environment.NewLine + "Single CDR: curl https://localhost:44356/read/GetSingleCDR?cdrId=5" + Environment.NewLine + "Call count/duration by date for caller id with optional type: curl https://localhost:44356/read/GetCallCountAndDurationByDateForCallerId/2016-08-01/2016-09-01/441269000000/2" + Environment.NewLine + "Most expensive call by date for caller id with optional type: curl https://localhost:44356/read/GetMostExpensiveCallCountByDateForCallerId/2016-08-01/2016-09-01/10/441269000000/2";
        }
    }
}
