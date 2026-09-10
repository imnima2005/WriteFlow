using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WriteFlow.Controllers
{
    public class ErrorHandlerController : Controller
    {
        [Route("ErrorHandler/{statusCode}")]
        public IActionResult Index(int statusCode)
        {
            switch (statusCode)
            {
                case 404:
                    ViewData["ErrorMessage"] = "صفحه مورد نظر شما پیدا نشد.";
                    return View("NotFound");

                case 403:
                    ViewData["ErrorMessage"] = "شما دسترسی به این بخش را ندارید.";
                    return View("AccessDenied");

                case 500:
                    ViewData["ErrorMessage"] = "خطایی در سرور رخ داده است. لطفاً بعداً تلاش کنید.";
                    return View("ServerError");

                default:
                    ViewData["ErrorMessage"] = $"خطایی با کد {statusCode} رخ داده است.";
                    return View("GeneralError");
            }
        }
    }
}
