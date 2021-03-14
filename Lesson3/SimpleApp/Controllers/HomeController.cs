using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace SimpleApp.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Download()
        {
            FileStream fileStream = System.IO.File.OpenRead("wwwroot/Lesson3.pdf");
            return File(fileStream, "application/pdf", "Lesson3.pdf");
        }

        public IActionResult Open()
        {
            FileStream fileStream = System.IO.File.OpenRead("wwwroot/Lesson3.pdf");
            return File(fileStream, "application/pdf");
        }
    }
}
