using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WriteFlow.Areas.Admin.Controllers
{
    public class DashboardController:AdminControllerBase
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
