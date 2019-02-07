using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Carpark_Project.Models;
using Carpark_Project.Services;
using Microsoft.AspNetCore.Mvc;

namespace Carpark_Project.Controllers
{
    [Route("api/[controller]")]
    public class CalculatePriceController : Controller
    {
        [HttpPost("[action]")]
        public IActionResult FinalPriceCarpark([FromBody]Times Times)
        {
            try
            {
                DateTime start = DateTime.Parse(Times.Start);
                DateTime end = DateTime.Parse(Times.End);
                var _priceCalculator = new PriceCalculator();
                var PriceObject = _priceCalculator.GetPrice(start, end);
                return Json(new { result = "success", PriceObject });
            }
            catch (Exception e)
            {
                return Json(new { Success = false, Message = "Error while retrieving Price details" });
            }
        }
    }
}

