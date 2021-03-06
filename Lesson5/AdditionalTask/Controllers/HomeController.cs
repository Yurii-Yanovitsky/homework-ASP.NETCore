using System;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AdditionalTask.Controllers
{
    public class HomeController : Controller
    {
        private const string key = "myKey";

        public IActionResult Index()
        {

            return View();
        }


        [HttpPost]
        public IActionResult Index(string value, DateTime dateTime)
        {
            CookieOptions options = new CookieOptions();
            options.Expires = dateTime;

            Response.Cookies.Append(key, value, options);

            return View();
        }

        public IActionResult Test()
        {
            string value = Request.Cookies[key];

            return View(value as object);
        }

    }
}
