using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DataLayer;
using DataLayer.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;

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
        [HttpGet]
        [Route("GetSingleCDR")]
        public IActionResult GetSingleCDR(int cdrId)
        {
           DataAccess dataAccess = new DataAccess();
           CDRModel cdr = dataAccess.GetCDRByID(_config, cdrId);
           return Json(new { data = cdr });
        }

        [HttpGet]
        [Route("GetCallCountAndDurationByDateForCallerId/{start}/{end}/{callerId}/{typeId}")]
        public IActionResult GetCallCountAndDurationByDateForCallerId(DateTime start, DateTime end, string callerId, int? typeId = null)
        {
            DataAccess dataAccess = new DataAccess();
            List<CDRModel> cdr = dataAccess.GetCallCountAndDurationByDateForCallerId(_config, start, end, callerId, typeId);
            return Json(new { data = cdr });
        }
        [HttpGet]
        [Route("GetCallCountAndDurationByDateForCallerId/{start}/{end}/{callerId}")]
        public IActionResult GetCallCountAndDurationByDateForCallerId(DateTime start, DateTime end, string callerId)
        {
            DataAccess dataAccess = new DataAccess();
            List<CDRModel> cdr = dataAccess.GetCallCountAndDurationByDateForCallerId(_config, start, end, callerId, null);
            return Json(new { data = cdr });
        }
        [HttpGet]
        [Route("GetMostExpensiveCallCountByDateForCallerId/{start}/{end}/{callerId}/{numberToReturn}/{typeId}")]
        public IActionResult GetMostExpensiveCallCountByDateForCallerId(DateTime start, DateTime end, string callerId, int numberToReturn, int? typeId = null)
        {
            DataAccess dataAccess = new DataAccess();
            List<CDRModel> cdr = dataAccess.GetMostExpensiveCallCountByDateForCallerId(_config, start, end, callerId, numberToReturn, typeId);
            return Json(new { data = cdr });
        }
        [HttpGet]
        [Route("GetMostExpensiveCallCountByDateForCallerId/{start}/{end}/{callerId}/{numberToReturn}/")]
        public IActionResult GetMostExpensiveCallCountByDateForCallerId(DateTime start, DateTime end, string callerId, int numberToReturn)
        {
            DataAccess dataAccess = new DataAccess();
            List<CDRModel> cdr = dataAccess.GetMostExpensiveCallCountByDateForCallerId(_config, start, end, callerId, numberToReturn, null);
            return Json(new { data = cdr });
        }


    }
}
