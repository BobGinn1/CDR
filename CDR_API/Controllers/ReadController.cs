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
    [Route("api/[controller]")]
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
        public IActionResult GetSingleCDR(int cdrId)
        {
           DataAccess dataAccess = new DataAccess();
           CDRModel cdr = dataAccess.GetCDRByID(_config, cdrId);
           return Json(new { data = cdr });
        }

       
    }
}
