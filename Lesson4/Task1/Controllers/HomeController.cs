using System;
using Microsoft.AspNetCore.Mvc;

namespace Task1.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            DateTime dateTime = DateTime.Now;

            return View(dateTime);
        }
    }
}
