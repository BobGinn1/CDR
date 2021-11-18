using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FileProcessor;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace CDR_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WriteController : ControllerBase
    {
        private readonly IConfiguration _config;

        public WriteController(IConfiguration config)
        {
            _config = config;
        }
        [HttpGet]
        public void ProcessCDRRecords()
        {
            CSVProcessor csvPro = new CSVProcessor();
            csvPro.LoadCDRData(_config);
        }

       
    }
}
