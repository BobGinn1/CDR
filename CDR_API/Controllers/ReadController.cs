using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DataLayer;
using DataLayer.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace CDR_API.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class ReadController : Controller
    {
        private readonly IConfiguration _config;

        public ReadController(IConfiguration config)
        {
            _config = config;
        }
        // GET: api/<ReadController>
        [HttpPost]
        [Route("GetSingleCDR")]
        public IActionResult GetSingleCDR(int cdrId)
        {
           DataAccess dataAccess = new DataAccess();
           CDRModel cdr = dataAccess.GetCDRByID(_config, cdrId);
           return Json(new { data = cdr });
        }

        [HttpPost]
        [Route("GetCallCountAndDurationByDateForCallerId")]
        public IActionResult GetCallCountAndDurationByDateForCallerId(DateTime start, DateTime end, string callerId, CallTypeModel type = null)
        {
            DataAccess dataAccess = new DataAccess();
            CDRModel cdr = dataAccess.GetCallCountAndDurationByDateForCallerId(_config, start, end, callerId, type);
            return Json(new { data = cdr });
        }

        [HttpPost]
        [Route("GetMostExpensiveCallCountByDateForCallerId")]
        public IActionResult GetMostExpensiveCallCountByDateForCallerId(DateTime start, DateTime end, string callerId, int numberToReturn, CallTypeModel type = null)
        {
            DataAccess dataAccess = new DataAccess();
            CDRModel cdr = dataAccess.GetMostExpensiveCallCountByDateForCallerId(_config, start, end, callerId, numberToReturn, type);
            return Json(new { data = cdr });
        }


    }
}
