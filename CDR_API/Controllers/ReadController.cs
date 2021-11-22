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

        [HttpPost]
        [Route("GetCallCountAndDurationByDateForCallerId")]
        public IActionResult GetCallCountAndDurationByDateForCallerId(string data)
        {

            var search = JsonConvert.DeserializeObject<CallCountSearchModel>(data);
            DataAccess dataAccess = new DataAccess();
            List<CDRModel> cdr = dataAccess.GetCallCountAndDurationByDateForCallerId(_config, search.Start, search.End, search.CallerId, search.Type);
            return Json(new { data = cdr });
        }
        [HttpGet]
        [Route("GetMostExpensiveCallCountByDateForCallerId")]
        public IActionResult GetMostExpensiveCallCountByDateForCallerId(string data)
        {
            var search = JsonConvert.DeserializeObject<MostExpensiveSearchModel>(data);
            DataAccess dataAccess = new DataAccess();
            List<CDRModel> cdr = dataAccess.GetMostExpensiveCallCountByDateForCallerId(_config, search.Start, search.End, search.CallerId, search.NumberToReturn, search.Type);
            return Json(new { data = cdr });
        }


    }
}
