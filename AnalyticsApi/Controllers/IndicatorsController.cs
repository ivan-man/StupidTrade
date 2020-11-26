using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AnalyticsApi.Controllers
{
    public class IndicatorsController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
